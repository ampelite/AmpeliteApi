using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers.Dailypo
{
    [Produces("application/json")]
    [Route("api/Dailypo/ProductTeams")]
    public class DailypoProductTeamsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoProductTeamsController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/DailypoProductTeams
        [HttpGet]
        public IEnumerable<DailypoProductTeam> GetDailypoProductTeam()
        {
            return _context.DailypoProductTeam;
        }

        // GET: api/DailypoProductTeams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailypoProductTeam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoProductTeam = await _context.DailypoProductTeam.SingleOrDefaultAsync(m => m.GptId == id);

            if (dailypoProductTeam == null)
            {
                return NotFound();
            }

            return Ok(dailypoProductTeam);
        }

        // PUT: api/DailypoProductTeams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailypoProductTeam([FromRoute] int id, [FromBody] DailypoProductTeam dailypoProductTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailypoProductTeam.GptId)
            {
                return BadRequest();
            }

            _context.Entry(dailypoProductTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailypoProductTeamExists(id))
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

        // POST: api/DailypoProductTeams
        [HttpPost]
        public async Task<IActionResult> PostDailypoProductTeam([FromBody] DailypoProductTeam dailypoProductTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DailypoProductTeam.Add(dailypoProductTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailypoProductTeam", new { id = dailypoProductTeam.GptId }, dailypoProductTeam);
        }

        // DELETE: api/DailypoProductTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailypoProductTeam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoProductTeam = await _context.DailypoProductTeam.SingleOrDefaultAsync(m => m.GptId == id);
            if (dailypoProductTeam == null)
            {
                return NotFound();
            }

            _context.DailypoProductTeam.Remove(dailypoProductTeam);
            await _context.SaveChangesAsync();

            return Ok(dailypoProductTeam);
        }

        private bool DailypoProductTeamExists(int id)
        {
            return _context.DailypoProductTeam.Any(e => e.GptId == id);
        }
    }
}