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
    public class GoodBrandCodesController : Controller
    {
        private readonly db_AmpeliteContext _context;
        private ICodePromotionService iCodeProService;
        private IGetTransactionInvService iGetTranInvService;

        public GoodBrandCodesController(
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService,
            IGetTransactionInvService iGetTransactionInvService
        )
        {
            _context = context;
            iCodeProService = iCodePromotionService;
            iGetTranInvService = iGetTransactionInvService;
        }

        // GET: api/GoodBrandCodes
        [HttpGet]
        public IEnumerable<GoodBrandCode> GetGoodBrandCode()
        {
            return _context.GoodBrandCode;
        }

        // GET: api/GoodBrandCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodBrandCode([FromRoute] int id)
        {
            try
            {
                var goodBrandCode = await _context.GoodBrandCode.SingleOrDefaultAsync(m => m.Id == id);

                var response = new GoodBrandResponse
                {
                    SubPromotionDropDowns = iCodeProService.SubPromotionDropDowns(),
                    GoodBrandCodeDropDowns = iGetTranInvService.GoodBrandDropDowns(),
                    goodBrand = goodBrandCode
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public class GoodBrandResponse
        {
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public List<DropDowns> GoodBrandCodeDropDowns { get; set; }
            public GoodBrandCode goodBrand { get; set; }
        }

        // PUT: api/GoodBrandCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoodBrandCode([FromRoute] int id, [FromBody] GoodBrandCode goodBrandCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goodBrandCode.Id)
            {
                return BadRequest();
            }

            var getBrand = await _context.GetTransactionInv
                .Where(w => w.GoodBrandCode == goodBrandCode.GoodBrandcode)
                .GroupBy(g => new { g.GoodBrandName })
                .Select(s => new { s.Key.GoodBrandName })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == goodBrandCode.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            goodBrandCode.GoodBrandName = getBrand[0].GoodBrandName;
            goodBrandCode.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.Entry(goodBrandCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodBrandCodeExists(id.ToString()))
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

        // POST: api/GoodBrandCodes
        [HttpPost]
        public async Task<IActionResult> PostGoodBrandCode([FromBody] GoodBrandCode goodBrandCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getBrand = await _context.GetTransactionInv
                .Where(w => w.GoodBrandCode == goodBrandCode.GoodBrandcode)
                .GroupBy(g => new { g.GoodBrandName })
                .Select(s => new { s.Key.GoodBrandName })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == goodBrandCode.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            goodBrandCode.GoodBrandName = getBrand[0].GoodBrandName;
            goodBrandCode.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.GoodBrandCode.Add(goodBrandCode);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GoodBrandCodeExists(goodBrandCode.GoodBrandcode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGoodBrandCode", new { id = goodBrandCode.GoodBrandcode }, goodBrandCode);
        }

        // DELETE: api/GoodBrandCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoodBrandCode([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goodBrandCode = await _context.GoodBrandCode.SingleOrDefaultAsync(m => m.GoodBrandcode == id);
            if (goodBrandCode == null)
            {
                return NotFound();
            }

            _context.GoodBrandCode.Remove(goodBrandCode);
            await _context.SaveChangesAsync();

            return Ok(goodBrandCode);
        }

        private bool GoodBrandCodeExists(string id)
        {
            return _context.GoodBrandCode.Any(e => e.GoodBrandcode == id);
        }
    }
}