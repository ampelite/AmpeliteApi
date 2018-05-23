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
    [Route("api/MasterGoodCateCode")]
    public class MasterGoodCateCodeController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public MasterGoodCateCodeController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/MasterGoodCateCode
        [HttpGet]
        public IEnumerable<GoodCateCode> GetGoodCateCode()
        {
            return _context.GoodCateCode;
        }

        // GET: api/MasterGoodCateCode/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodCateCode([FromRoute] int id)
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

            return Ok(goodCateCode);
        }

        // PUT: api/MasterGoodCateCode/5
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

        // POST: api/MasterGoodCateCode
        [HttpPost]
        public async Task<IActionResult> PostGoodCateCode([FromBody] GoodCateCode goodCateCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GoodCateCode.Add(goodCateCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoodCateCode", new { id = goodCateCode.Id }, goodCateCode);
        }

        // DELETE: api/MasterGoodCateCode/5
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