using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

using AmpeliteApi.Controllers.Pivot;

using AmpeliteApi.Models;

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
        public IEnumerable<object> Get(DateTime Date, String GroupCode, String Unit)
        {
            var p1 = Date.Date;
            var p2 = GroupCode;
            var p3 = Unit;

            var Result = _context
                .DailypoGraphProduct
                .FromSql("sp_DAILYPO_GraphProduct @p0, @p1, @p2", parameters: new[] { p1.ToString("yyyy-MM-dd"), p2, p3 })
                .ToArray();

            var ListProduct = Result.Where(p => p.Type.Equals("product")).ToList();
            var ListSum = Result.Where(p => p.Type.Equals("sum")).ToList();
            var ListAccu = Result.Where(p => p.Type.Equals("accu")).ToList();
            var ListAvg = Result.Where(p => p.Type.Equals("avg")).ToList();

            var ListReturn = new List<object>();
            var Cate = new Categories();

            // select และ group TeamName ออกมา
            var ListTeamName = ListProduct.GroupBy(g => g.TeamName).Select(u => u.Key).ToList();

            foreach(string Name in ListTeamName)
            {
                var ListTeam = ListProduct.Where(u => u.TeamName.Equals(Name)).ToList();
                Cate = new Categories();
                Cate.Type = "product";
                Cate.Name = Name;
                Cate.Unit = ListTeam.Select(u => Double.Parse(u.Unit.ToString())).ToList();
                ListReturn.Add(Cate);                
            }

            Cate = new Categories();
            Cate.Type = "sum";
            Cate.Name = "sum";
            Cate.Unit = ListSum.Select(u => Double.Parse(u.Unit.ToString())).ToList();
            ListReturn.Add(Cate);

            Cate = new Categories();
            Cate.Type = "accu";
            Cate.Name = "accu";
            Cate.Unit = ListAccu.Select(u => Double.Parse(u.Unit.ToString())).ToList();
            ListReturn.Add(Cate);

            Cate = new Categories();
            Cate.Type = "avg";
            Cate.Name = "avg";
            Cate.Unit = ListAvg.Select(u => Double.Parse(u.Unit.ToString())).ToList();
            ListReturn.Add(Cate);

            //var pivotArray = Result.ToPivotArray(
            //item => item.Day,
            //item => item.Type,
            //items => items.Any() ? items.Sum(x => Double.Parse(x.Unit.ToString())) : 0
            //);

            return ListReturn;
        }
    }

    public class Categories
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Double> Unit { get; set; }
    }
}
