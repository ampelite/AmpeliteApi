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
    [Route("api/MasterGoodPattn")]
    public class MasterGoodPattnController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public MasterGoodPattnController(db_AmpeliteContext context)
        {
            _context = context;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var saleproGoodPattn = await _context.SaleproGoodPattn.SingleOrDefaultAsync(m => m.Id == id);

            if (saleproGoodPattn == null)
            {
                return NotFound();
            }

            return Ok(saleproGoodPattn);
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