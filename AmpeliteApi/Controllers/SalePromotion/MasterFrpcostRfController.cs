using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using AmpeliteApi.Services.SalePromotion;

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/SalePromotion/[controller]")]
    public class MasterFrpcostRfController : Controller
    {
        private db_AmpeliteContext _context;
        private ICodePromotionService iCodeProService;
        private IGetTransactionInvService iGetTranInvService;

        public MasterFrpcostRfController(
            db_AmpeliteContext context,
            ICodePromotionService iCodePromotionService,
            IGetTransactionInvService iGetTransactionInvService
        )
        {
            _context = context;
            iCodeProService = iCodePromotionService;
            iGetTranInvService = iGetTransactionInvService;
        }

        // GET: api/MasterFrpcostRf
        [HttpGet]
        public async Task<IActionResult> GetSaleproFrpcostRf()
        {
            var query = await (from costRf in _context.SaleproFrpcostRf
                               join promotion in _context.CodePromotion
                               on costRf.SubId equals promotion.SubId
                               select new
                               { costRf.Id
                                ,costRf.GoodCateCode
                                ,costRf.GoodCateName
                                ,costRf.Amount
                                ,costRf.Rf
                                ,costRf.TrsP
                                ,costRf.SubId
                                ,costRf.SubCodePro
                                ,costRf.MaxLength
                                ,costRf.Status
                                ,promotion.SubPromotion
                               }).ToListAsync();
            return Ok(query);
        }

        // GET: api/MasterFrpcostRf/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleproFrpcostRf(int id)
        {
            try
            {
                var saleproFrpcostRf = await _context.SaleproFrpcostRf.SingleOrDefaultAsync(m => m.Id == id);

                var response = new FrpcostRfResponse
                {
                    ProductDropDowns = iGetTranInvService.ProductDropDowns(),
                    SubPromotionDropDowns = iCodeProService.SubPromotionDropDowns(),
                    promotionCostRF = saleproFrpcostRf
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        public class FrpcostRfResponse
        {
            public List<DropDowns> ProductDropDowns { get; set; }
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public SaleproFrpcostRf promotionCostRF { get; set; }
        }

        // PUT: api/MasterFrpcostRf/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaleproFrpcostRf([FromRoute] int id, [FromBody] SaleproFrpcostRf saleproFrpcostRf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saleproFrpcostRf.Id)
            {
                return BadRequest();
            }

            var getProduct = await _context.GetTransactionInv
                .Where(w => w.ProductCode == saleproFrpcostRf.GoodCateCode)
                .GroupBy(g => new { g.Product })
                .Select(s => new { s.Key.Product })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == saleproFrpcostRf.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            saleproFrpcostRf.GoodCateName = getProduct[0].Product;
            saleproFrpcostRf.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.Entry(saleproFrpcostRf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleproFrpcostRfExists(id))
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

        // POST: api/MasterFrpcostRf
        [HttpPost]
        public async Task<IActionResult> PostSaleproFrpcostRf([FromBody] SaleproFrpcostRf saleproFrpcostRf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getProduct = await _context.GetTransactionInv
                .Where(w => w.ProductCode == saleproFrpcostRf.GoodCateCode)
                .GroupBy(g => new { g.Product })
                .Select(s => new { s.Key.Product })
                .ToListAsync();

            var getSubCodePro = await _context.CodePromotion
                .Where(w => w.SubId == saleproFrpcostRf.SubId)
                .GroupBy(g => new { g.SubCodePro })
                .Select(s => new { s.Key.SubCodePro })
                .ToListAsync();

            saleproFrpcostRf.GoodCateName = getProduct[0].Product;
            saleproFrpcostRf.SubCodePro = getSubCodePro[0].SubCodePro;

            _context.SaleproFrpcostRf.Add(saleproFrpcostRf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaleproFrpcostRf", new { id = saleproFrpcostRf.Id }, saleproFrpcostRf);
        }

        // DELETE: api/MasterFrpcostRf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleproFrpcostRf([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleproFrpcostRf = await _context.SaleproFrpcostRf.SingleOrDefaultAsync(m => m.Id == id);
            if (saleproFrpcostRf == null)
            {
                return NotFound();
            }

            _context.SaleproFrpcostRf.Remove(saleproFrpcostRf);
            await _context.SaveChangesAsync();

            return Ok(saleproFrpcostRf);
        }

        private bool SaleproFrpcostRfExists(int id)
        {
            return _context.SaleproFrpcostRf.Any(e => e.Id == id);
        }
    }
}