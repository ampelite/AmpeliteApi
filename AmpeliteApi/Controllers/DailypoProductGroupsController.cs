using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers
{
    [Produces("application/json")]
    [Route("api/DailypoProductGroups")]
    public class DailypoProductGroupsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoProductGroupsController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/DailypoProductGroups
        [HttpGet]
        public IEnumerable<DailypoProductGroup> GetDailypoProductGroup()
        {
            return _context.DailypoProductGroup;
        }

        // GET: api/DailypoProductGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailypoProductGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoProductGroup = await _context.DailypoProductGroup.SingleOrDefaultAsync(m => m.Id == id);

            if (dailypoProductGroup == null)
            {
                return NotFound();
            }

            return Ok(dailypoProductGroup);
        }

        // PUT: api/DailypoProductGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailypoProductGroup([FromRoute] int id, [FromBody] DailypoProductGroup dailypoProductGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailypoProductGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(dailypoProductGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailypoProductGroupExists(id))
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

        // POST: api/DailypoProductGroups
        [HttpPost]
        public async Task<IActionResult> PostDailypoProductGroup([FromBody] DailypoProductGroup dailypoProductGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DailypoProductGroup.Add(dailypoProductGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailypoProductGroup", new { id = dailypoProductGroup.Id }, dailypoProductGroup);
        }

        // DELETE: api/DailypoProductGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailypoProductGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoProductGroup = await _context.DailypoProductGroup.SingleOrDefaultAsync(m => m.Id == id);
            if (dailypoProductGroup == null)
            {
                return NotFound();
            }

            _context.DailypoProductGroup.Remove(dailypoProductGroup);
            await _context.SaveChangesAsync();

            return Ok(dailypoProductGroup);
        }

        private bool DailypoProductGroupExists(int id)
        {
            return _context.DailypoProductGroup.Any(e => e.Id == id);
        }
    }
}