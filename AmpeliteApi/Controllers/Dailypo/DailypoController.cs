using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Models;

namespace AmpeliteApi.Controllers.Dailypo
{
    [Produces("application/json")]
    [Route("api/Dailypo")]
    public class DailypoController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/Dailypo
        [HttpGet]
        public IActionResult Get()
        {
            var ProductTeam = _context.DailypoGroupReport.ToList();
            var GroupUnit = _context.DailypoGroupUnit.ToList();
            var obj = new Dictionary<string, object> {
                    {"productTeam", ProductTeam},
                    {"groupUnit", GroupUnit}
                };
            return Ok(obj.ToList());
        }

        // GET: api/Dailypo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dailypo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dailypo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
