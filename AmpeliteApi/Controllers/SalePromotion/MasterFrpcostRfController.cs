using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/MasterFrpcostRf")]
    public class MasterFrpcostRfController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public MasterFrpcostRfController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/MasterFrpcostRf
        [HttpGet]
        public IEnumerable<SaleproFrpcostRf> GetSaleproFrpcostRf()
        {
            return _context.SaleproFrpcostRf;
        }

        // GET: api/MasterFrpcostRf/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleproFrpcostRf([FromRoute] int id)
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

            return Ok(saleproFrpcostRf);
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