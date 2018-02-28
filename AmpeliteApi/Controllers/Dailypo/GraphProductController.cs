using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AmpeliteApi.Controllers.Dailypo
{
    [Produces("application/json")]
    [Route("api/GraphProduct")]
    public class GraphProductController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public GraphProductController(db_AmpeliteContext context)
        {
            _context = context;
        }

        // GET: api/GraphProduct
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var s = _context.Database.SqlQuery<DailypoGraphProduct>("AllSalesPeople");
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
 //               @EDate AS DATETIME,
	//@GroupCode as VARCHAR(25),
	//@Unit
                command.CommandText = "dbo.sp_DAILYPO_GraphProduct @EDate ='2018-02-27', @GroupCode ='fibre', @Unit ='1m'";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                   while(result.HasRows)
                    {
                        
                    }
                }
                _context.Database.CloseConnection();
            }
            //var product = _context
            //    .DailypoGraphProduct
            //    .FromSql("EXECUTE dbo.sp_DAILYPO_GraphProduct  @productCategory", productCategory)
            //    .ToList();
            return new string[] { "value1", "value2" };
        }

        // GET: api/GraphProduct/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/GraphProduct
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/GraphProduct/5
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
