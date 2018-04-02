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
    [Route("api/Dailypo/DetailDaily")]
    public class DailypoDetailDailiesController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoDetailDailiesController(db_AmpeliteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string groupCode, string teamName, DateTime sDate)
        {
            if (groupCode == null || teamName == null || sDate == null)
            {
                return NotFound();
            }

            try
            {
                var p0 = groupCode;
                var p1 = teamName;
                var p2 = sDate;

                var stored = (groupCode == "saleteam") ? "sp_DAILYPO_RptByTeamSale" : "sp_DAILYPO_RptByProduct";

                List<DailypoDetailDaily> list = await _context.DailypoDetailDaily
                    .FromSql(stored + " @p0, @p1, @p2", parameters: new[] { p0, p1, p2.ToString("yyyy-MM-dd") })
                    .ToListAsync();
                return Ok(list);

            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
         
        }
    }
}