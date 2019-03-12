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
    public class MasterGoodPattnController : Controller
    {
        private readonly db_AmpeliteContext _context;
        private ICodePromotionService iCodeProService;
        private ICodePattnService iPattnService;
        private ICodeClassService iClassService;

        public MasterGoodPattnController(
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService,
            ICodePattnService iCodePattnService,
            ICodeClassService iCodeClassService
        )
        {
            _context = context;
            iCodeProService = iCodePromotionService;
            iPattnService = iCodePattnService;
            iClassService = iCodeClassService;
        }

        // GET: api/MasterGoodPattn
        [HttpGet]
        public IEnumerable<SaleproGoodPattn> GetSaleproGoodPattn()
        {
            return _context.SaleproGoodPattn;
        }

        // GET: api/MasterGoodPattn/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleproGoodPattn([FromRoute] int id)
        {
            try
            {
                var saleproGoodPattn = await _context.SaleproGoodPattn.SingleOrDefaultAsync(m => m.Id == id);

                var response = new SpecialResponse
                {
                    SubPromotionDropDowns = iCodeProService.SubPromotionDropDowns(),
                    GoodPattnDropDowns = iPattnService.PattnDropDowns(),
                    GoodClassDropDowns = iClassService.ClassDropDowns(),
                    promotionSpecial = saleproGoodPattn
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public class SpecialResponse
        {
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public List<DropDowns> GoodPattnDropDowns { get; set; }
            public List<DropDowns> GoodClassDropDowns { get; set; }
            public SaleproGoodPattn promotionSpecial { get; set; }
        }

        // PUT: api/MasterGoodPattn/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleproGoodPattn([FromRoute] int id, [FromBody] SaleproGoodPattn saleproGoodPattn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saleproGoodPattn.Id)
            {
                return BadRequest();
            }

            var getGoodPattn = await _context.GetTransactionInv
                .Where(w => w.GoodPattnCode == saleproGoodPattn.GoodPattnCode)
                .GroupBy(g => new { g.GoodPattnName })
                .Select(s => new { s.Key.GoodPattnName })
                .ToListAsync();

            var getGoodClass = await _context.GetTransactionInv
                .Where(w => w.GoodClassCode == saleproGoodPattn.GoodClassCode)
                .GroupBy(g => new { g.GoodClassName })
                .Select(s => new { s.Key.GoodClassName })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == saleproGoodPattn.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            saleproGoodPattn.GoodPattnName = getGoodPattn[0].GoodPattnName;
            saleproGoodPattn.GoodClassName = getGoodClass[0].GoodClassName;
            saleproGoodPattn.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.Entry(saleproGoodPattn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleproGoodPattnExists(id))
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

        // POST: api/MasterGoodPattn
        [HttpPost]
        public async Task<IActionResult> PostSaleproGoodPattn([FromBody] SaleproGoodPattn saleproGoodPattn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getGoodPattn = await _context.GetTransactionInv
                .Where(w => w.GoodPattnCode == saleproGoodPattn.GoodPattnCode)
                .GroupBy(g => new { g.GoodPattnName })
                .Select(s => new { s.Key.GoodPattnName })
                .ToListAsync();

            var getGoodClass = await _context.GetTransactionInv
                .Where(w => w.GoodClassCode == saleproGoodPattn.GoodClassCode)
                .GroupBy(g => new { g.GoodClassName })
                .Select(s => new { s.Key.GoodClassName })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == saleproGoodPattn.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            saleproGoodPattn.GoodPattnName = getGoodPattn[0].GoodPattnName;
            saleproGoodPattn.GoodClassName = getGoodClass[0].GoodClassName;
            saleproGoodPattn.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.SaleproGoodPattn.Add(saleproGoodPattn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleproGoodPattn", new { id = saleproGoodPattn.Id }, saleproGoodPattn);
        }

        // DELETE: api/MasterGoodPattn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleproGoodPattn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleproGoodPattn = await _context.SaleproGoodPattn.SingleOrDefaultAsync(m => m.Id == id);
            if (saleproGoodPattn == null)
            {
                return NotFound();
            }

            _context.SaleproGoodPattn.Remove(saleproGoodPattn);
            await _context.SaveChangesAsync();

            return Ok(saleproGoodPattn);
        }

        private bool SaleproGoodPattnExists(int id)
        {
            return _context.SaleproGoodPattn.Any(e => e.Id == id);
        }
    }
}