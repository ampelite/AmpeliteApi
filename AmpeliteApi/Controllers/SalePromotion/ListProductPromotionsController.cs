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
    public class ListProductPromotionsController : Controller
    {
        private readonly db_AmpeliteContext _context;
        private ICodePromotionService iCodeProService;
        private IGetTransactionInvService iGetTranInvService;

        public ListProductPromotionsController(
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService,
            IGetTransactionInvService iGetTransactionInvService
        )
        {
            _context = context;
            iCodeProService = iCodePromotionService;
            iGetTranInvService = iGetTransactionInvService;
        }

        // GET: api/ListProductPromotions
        [HttpGet]
        public async Task<IActionResult> GetListProductPromotion()
        {
            var query = await (from product in _context.ListProductPromotion
                               join promotion in _context.CodePromotion
                               on product.SubId equals promotion.SubId

                               select new
                               { product.Id
                                ,product.GoodId
                                ,product.GoodCode
                                ,product.SubId
                                ,product.SubCodePro
                                ,promotion.SubPromotion
                               }).ToListAsync();
            return Ok(query);
        }

        // GET: api/ListProductPromotions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListProductPromotion([FromRoute] int id)
        {
            try
            {
                var listProductPromotion = await _context.ListProductPromotion.SingleOrDefaultAsync(m => m.Id == id);

                var response = new ListProductResponse
                {
                    SubPromotionDropDowns = iCodeProService.SubPromotionDropDowns(),
                    GoodCodeDropDowns = iGetTranInvService.GoodCodeDropDowns(),
                    GoodCode = listProductPromotion
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public class ListProductResponse
        {
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public List<DropDowns> GoodCodeDropDowns { get; set; }
            public ListProductPromotion GoodCode { get; set; }
        }

        // PUT: api/ListProductPromotions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListProductPromotion([FromRoute] int id, [FromBody] ListProductPromotion listProductPromotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listProductPromotion.Id)
            {
                return BadRequest();
            }

            var getCode = await _context.GetTransactionInv
                .Where(w => w.GoodId == listProductPromotion.GoodId)
                .GroupBy(g => new { g.GoodCode })
                .Select(s => new { s.Key.GoodCode })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == listProductPromotion.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            listProductPromotion.GoodCode = getCode[0].GoodCode;
            listProductPromotion.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.Entry(listProductPromotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListProductPromotionExists(id.ToString()))
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

        // POST: api/ListProductPromotions
        [HttpPost]
        public async Task<IActionResult> PostListProductPromotion([FromBody] ListProductPromotion listProductPromotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getCode = await _context.GetTransactionInv
                .Where(w => w.GoodId == listProductPromotion.GoodId)
                .GroupBy(g => new { g.GoodCode })
                .Select(s => new { s.Key.GoodCode })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == listProductPromotion.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            listProductPromotion.GoodCode = getCode[0].GoodCode;
            listProductPromotion.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.ListProductPromotion.Add(listProductPromotion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ListProductPromotionExists(listProductPromotion.GoodId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetListProductPromotion", new { id = listProductPromotion.GoodId }, listProductPromotion);
        }

        // DELETE: api/ListProductPromotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListProductPromotion([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listProductPromotion = await _context.ListProductPromotion.SingleOrDefaultAsync(m => m.GoodId == id);
            if (listProductPromotion == null)
            {
                return NotFound();
            }

            _context.ListProductPromotion.Remove(listProductPromotion);
            await _context.SaveChangesAsync();

            return Ok(listProductPromotion);
        }

        private bool ListProductPromotionExists(string id)
        {
            return _context.ListProductPromotion.Any(e => e.GoodId == id);
        }
    }
}