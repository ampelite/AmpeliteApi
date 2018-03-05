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

            int Day = 1;

            Double Accu = 0.00;
            Double Avg = 0.00;
            var Cate = new Categories();
            var ListCate = new List<dynamic>();

            for (int i = 0; i < Result.Count(); i++)
            {
                if (Result[i].TeamCode.ToLower() == "total")
                {
                    int WeekDay = Result[i].WeekDay;
                    Double Total = Double.Parse(Result[i].Unit.ToString());

                    if (Day <= Date.Day) { Accu += Total; }

                    // กรณีที่ไม่ใช่วันเสาร์ อาทิตย์
                    if (WeekDay != 1 && WeekDay != 7)
                    {
                        // จะคำนวณ Avg ก็ต่อเมื่อวันที่ไม่เกิน param.Date.Day
                        if (Day <= Date.Day) { Avg = Accu / Day; }
                        Day++;
                    }

                    if (Day <= Date.Day)
                    {
                        Cate.Accu = Accu;
                        Cate.Avg = Avg;
                    }

                    Cate.WeekDay = WeekDay;
                    Cate.Day = Result[i].Day;
                    Cate.TotalUnit = Total;
                    ListCate.Add(Cate);
                }
            }

            var all = new List<dynamic>();
            all.Add(Result);
            all.Add(Cate);

            var pivotArray = Result.ToPivotArray(
            item => item.Day,
            item => item.TeamName,
            items => items.Any() ? items.Sum(x => x.Unit) : 0
            );

            

            return pivotArray;
        }

    }

    public class DalipoGraphParam
    {
        public DateTime Date { get; set; }
        public String GroupCode { get; set; }
        public String Unit { get; set; }
    }

    public class Categories
    {
        public int WeekDay { get; set; }
        public int Day { get; set; }
        public double TotalUnit { get; set; }
        public double Accu { get; set; }
        public double Avg { get; set; }
    }

}
