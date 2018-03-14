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
    [Route("api/DailypoGroupUnits")]
    public class DailypoGroupUnitsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoGroupUnitsController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/DailypoGroupUnits
        [HttpGet]
        public IEnumerable<DailypoGroupUnit> GetDailypoGroupUnit()
        {
            return _context.DailypoGroupUnit;
        }

        // GET: api/DailypoGroupUnits/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailypoGroupUnit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoGroupUnit = await _context.DailypoGroupUnit.SingleOrDefaultAsync(m => m.UnitId == id);

            if (dailypoGroupUnit == null)
            {
                return NotFound();
            }

            return Ok(dailypoGroupUnit);
        }
        
        [HttpGet("byGroupCode/{groupCode}")]
        public async Task<IActionResult> GetDailypoGroupUnitByGroupCode([FromRoute]  string groupCode)
        {
            //[FromRoute]  string groupCode
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dailypoGroupUnit = await (from p in _context.DailypoGroupUnit
                                              where (p.GroupCode == groupCode)
                                              select new DailypoGroupUnit
                                              {
                                                  UnitId = p.UnitId,
                                                  GroupCode = p.GroupCode,
                                                  UnitCode = p.UnitCode,
                                                  UnitValue = p.UnitValue,
                                                  UnitName = p.UnitName,
                                                  UnitTitle = p.UnitTitle
                                              }).ToListAsync();

                if (dailypoGroupUnit == null)
                {
                    return NotFound();
                }
                return Ok(dailypoGroupUnit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        // PUT: api/DailypoGroupUnits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailypoGroupUnit([FromRoute] int id, [FromBody] DailypoGroupUnit dailypoGroupUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailypoGroupUnit.UnitId)
            {
                return BadRequest();
            }

            _context.Entry(dailypoGroupUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailypoGroupUnitExists(id))
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

        // POST: api/DailypoGroupUnits
        [HttpPost]
        public async Task<IActionResult> PostDailypoGroupUnit([FromBody] DailypoGroupUnit dailypoGroupUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DailypoGroupUnit.Add(dailypoGroupUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailypoGroupUnit", new { id = dailypoGroupUnit.UnitId }, dailypoGroupUnit);
        }

        // DELETE: api/DailypoGroupUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailypoGroupUnit([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dailypoGroupUnit = await _context.DailypoGroupUnit.SingleOrDefaultAsync(m => m.UnitId == id);
            if (dailypoGroupUnit == null)
            {
                return NotFound();
            }

            _context.DailypoGroupUnit.Remove(dailypoGroupUnit);
            await _context.SaveChangesAsync();

            return Ok(dailypoGroupUnit);
        }

        private bool DailypoGroupUnitExists(int id)
        {
            return _context.DailypoGroupUnit.Any(e => e.UnitId == id);
        }
    }
}