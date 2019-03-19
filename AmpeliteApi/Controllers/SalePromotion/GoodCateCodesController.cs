using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using AmpeliteApi.Services.SalePromotion;

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/SalePromotion/[controller]")]
    public class GoodCateCodesController : Controller
    {
        private db_AmpeliteContext _context;
        private ICodePromotionService iCodeProService;
        private IGetTransactionInvService iGetTranInvService;

        public GoodCateCodesController(
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService,
            IGetTransactionInvService iGetTransactionInvService
        )
        {
            _context = context;
            iCodeProService = iCodePromotionService;
            iGetTranInvService = iGetTransactionInvService;
        }

        // GET: api/GoodCateCodes
        [HttpGet]
        public async Task<IActionResult> GetGoodCateCode()
        {
            var query = await (from cate in _context.GoodCateCode
                               join promotion in _context.CodePromotion
                               on cate.SubId equals promotion.SubId
                               select new
                               { cate.Id
                                ,cate.GoodCatecode
                                ,cate.GoodCateName
                                ,cate.SubId
                                ,cate.SubCodePro
                                ,cate.Status
                                ,promotion.SubPromotion
                               }).ToListAsync();
            return Ok(query);
            
        }

        // GET: api/GoodCateCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodCateCode([FromRoute] int id)
        {
            try
            {
                var goodCateCode = await _context.GoodCateCode.SingleOrDefaultAsync(m => m.Id == id);

                var response = new GoodCateResponse
                {
                    SubPromotionDropDowns = iCodeProService.SubPromotionDropDowns(),
                    GoodCateCodeDropDowns = iGetTranInvService.ProductDropDowns(),
                    goodCate = goodCateCode
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public class GoodCateResponse
        {
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public List<DropDowns> GoodCateCodeDropDowns { get; set; }
            public GoodCateCode goodCate { get; set; }
        }

        // PUT: api/GoodCateCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodCateCode([FromRoute] int id, [FromBody] GoodCateCode goodCateCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodCateCode.Id)
            {
                return BadRequest();
            }

            var getProduct = await _context.GetTransactionInv
                .Where(w => w.ProductCode == goodCateCode.GoodCatecode)
                .GroupBy(g => new { g.Product })
                .Select(s => new { s.Key.Product })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == goodCateCode.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            goodCateCode.GoodCateName = getProduct[0].Product;
            goodCateCode.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.Entry(goodCateCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodCateCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GoodCateCodes
        [HttpPost]
        public async Task<IActionResult> PostGoodCateCode([FromBody] GoodCateCode goodCateCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getCate = await _context.GetTransactionInv
                .Where(w => w.ProductCode == goodCateCode.GoodCatecode)
                .GroupBy(g => new { g.Product })
                .Select(s => new { s.Key.Product })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == goodCateCode.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            goodCateCode.GoodCateName = getCate[0].Product;
            goodCateCode.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.GoodCateCode.Add(goodCateCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodCateCode", new { id = goodCateCode.Id }, goodCateCode);
        }

        // DELETE: api/GoodCateCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodCateCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goodCateCode = await _context.GoodCateCode.SingleOrDefaultAsync(m => m.Id == id);
            if (goodCateCode == null)
            {
                return NotFound();
            }

            _context.GoodCateCode.Remove(goodCateCode);
            await _context.SaveChangesAsync();

            return Ok(goodCateCode);
        }

        private bool GoodCateCodeExists(int id)
        {
            return _context.GoodCateCode.Any(e => e.Id == id);
        }
    }
}