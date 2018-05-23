using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers.Dailypo
{
    [Produces("application/json")]
    [Route("api/Dailypo/GroupReports")]
    public class DailypoGroupReportsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoGroupReportsController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/DailypoGroupReports
        [HttpGet]
        public IEnumerable<DailypoGroupReport> GetDailypoGroupReport()
        {
            return _context.DailypoGroupReport;
        }

        // GET: api/DailypoGroupReports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailypoGroupReport([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoGroupReport = await _context.DailypoGroupReport.SingleOrDefaultAsync(m => m.GroupCode == id);

            if (dailypoGroupReport == null)
            {
                return NotFound();
            }

            return Ok(dailypoGroupReport);
        }

       
        // PUT: api/DailypoGroupReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailypoGroupReport([FromRoute] string id, [FromBody] DailypoGroupReport dailypoGroupReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailypoGroupReport.GroupCode)
            {
                return BadRequest();
            }

            _context.Entry(dailypoGroupReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailypoGroupReportExists(id))
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

        // POST: api/DailypoGroupReports
        [HttpPost]
        public async Task<IActionResult> PostDailypoGroupReport([FromBody] DailypoGroupReport dailypoGroupReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DailypoGroupReport.Add(dailypoGroupReport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DailypoGroupReportExists(dailypoGroupReport.GroupCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDailypoGroupReport", new { id = dailypoGroupReport.GroupCode }, dailypoGroupReport);
        }

        // DELETE: api/DailypoGroupReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailypoGroupReport([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoGroupReport = await _context.DailypoGroupReport.SingleOrDefaultAsync(m => m.GroupCode == id);
            if (dailypoGroupReport == null)
            {
                return NotFound();
            }

            _context.DailypoGroupReport.Remove(dailypoGroupReport);
            await _context.SaveChangesAsync();

            return Ok(dailypoGroupReport);
        }

        private bool DailypoGroupReportExists(string id)
        {
            return _context.DailypoGroupReport.Any(e => e.GroupCode == id);
        }
    }
}