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
    [Route("api/HrEmployees")]
    public class HrEmployeesController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public HrEmployeesController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/HrEmployees
        [HttpGet]
        public IEnumerable<HrEmployee> GetHrEmployee()
        {
            return _context.HrEmployee;
        }

        // GET: api/HrEmployees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHrEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hrEmployee = await _context.HrEmployee.SingleOrDefaultAsync(m => m.SEmpId == id);

            if (hrEmployee == null)
            {
                return NotFound();
            }

            return Ok(hrEmployee);
        }

        // PUT: api/HrEmployees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHrEmployee([FromRoute] int id, [FromBody] HrEmployee hrEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hrEmployee.SEmpId)
            {
                return BadRequest();
            }

            _context.Entry(hrEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HrEmployeeExists(id))
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

        // POST: api/HrEmployees
        [HttpPost]
        public async Task<IActionResult> PostHrEmployee([FromBody] HrEmployee hrEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HrEmployee.Add(hrEmployee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HrEmployeeExists(hrEmployee.SEmpId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHrEmployee", new { id = hrEmployee.SEmpId }, hrEmployee);
        }

        // DELETE: api/HrEmployees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHrEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hrEmployee = await _context.HrEmployee.SingleOrDefaultAsync(m => m.SEmpId == id);
            if (hrEmployee == null)
            {
                return NotFound();
            }

            _context.HrEmployee.Remove(hrEmployee);
            await _context.SaveChangesAsync();

            return Ok(hrEmployee);
        }

        private bool HrEmployeeExists(int id)
        {
            return _context.HrEmployee.Any(e => e.SEmpId == id);
        }
    }
}