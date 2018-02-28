using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data.Common;

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
        public IEnumerable<string> GetAsync()
        {
            var userType = _context.Set<DailypoGraphProduct>().FromSql("dbo.sp_DAILYPO_GraphProduct");

            //List<DailypoGraphProduct> groups = new List<DailypoGraphProduct>();
            //var conn = _context.Database.GetDbConnection();
            //try
            //{
            //    await conn.OpenAsync();
            //    using (var command = conn.CreateCommand())
            //    {
            //        string query = "exec dbo.sp_DAILYPO_GraphProduct";
            //        command.CommandText = query;
            //        DbDataReader reader = await command.ExecuteReaderAsync();

            //        if (reader.HasRows)
            //        {
            //            while (await reader.ReadAsync())
            //            {
            //                var row = new DailypoGraphProduct {
            //                    EDate = reader.GetDateTime(0),
            //                    _Day = reader.GetInt32(1),
            //                    GroupCode = reader.GetString(2),
            //                    TeamName = reader.GetString(3),
            //                    Unit = reader.GetDouble(4)
            //                };
            //                groups.Add(row);
            //            }
            //        }
            //        reader.Dispose();
            //    }
            //}
            //finally
            //{
            //    conn.Close();
            //}

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
