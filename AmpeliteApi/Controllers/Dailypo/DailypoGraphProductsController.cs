﻿using System;
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
    [Route("api/Dailypo/Graph")]
    public class DailypoGraphProductsController : Controller
    {
        private readonly db_AmpeliteContext _context;

        public DailypoGraphProductsController(db_AmpeliteContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Graph(DateTime Date, String GroupCode, String Unit)
        {
            var p1 = Date.Date;
            var p2 = GroupCode;
            var p3 = Unit;

            var stored = (p2 == "saleteam") ? "sp_DAILYPO_GraphTeamSale" : "sp_DAILYPO_GraphProduct";

            var Result = await _context
                .DailypoGraphProduct
                .FromSql(stored + " @p0, @p1, @p2", parameters: new[] { p1.ToString("yyyy-MM-dd"), p2, p3 })
                .ToListAsync();

            var ListProduct = Result.Where(p => p.Type.Equals("product")).ToList();

            var ListSum = Result.Where(p => p.Type.Equals("sum")).OrderBy(x => x.Day).ToList();
            var ListAccu = Result.Where(p => p.Type.Equals("accu")).OrderBy(x => x.Day).ToList();
            var ListAvg = Result.Where(p => p.Type.Equals("avg")).OrderBy(x => x.Day).ToList();

            var ListReturn = new List<object>();
            var Cate = new Categories();

            // select และ group TeamName ออกมา
            var ListTeamName = ListProduct.GroupBy(g => g.TeamName).Select(u => u.Key).ToList();

            foreach (string Name in ListTeamName)
            {
                var ListTeam = ListProduct.Where(u => u.TeamName.Equals(Name)).OrderBy(x => x.Day).ToList();
                Cate = new Categories();
                Cate.Type = "product";
                Cate.Name = Name;
                Cate.Unit = ListTeam.Select(u => (double?)(u.Unit)).ToArray();
                ListReturn.Add(Cate);
            }

            Cate = new Categories();
            Cate.Type = "sum";
            Cate.Name = "Sum";
            Cate.Unit = ListSum.Select(u => (double?)(u.Unit)).ToArray();
            ListReturn.Add(Cate);

            Cate = new Categories();
            Cate.Type = "accu";
            Cate.Name = "Accu";
            Cate.Unit = ListAccu.Select(u => (double?)(u.Unit)).ToArray();
            ListReturn.Add(Cate);

            Cate = new Categories();
            Cate.Type = "avg";
            Cate.Name = "Avg";
            Cate.Unit = ListAvg.Select(u => (double?)(u.Unit)).ToArray();
            ListReturn.Add(Cate);

            return Ok(ListReturn);
        }

        public class Categories
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public double?[] Unit { get; set; }
        }

        public class Units
        {
            public int Day { get; set; }
            public double Unit { get; set; }
        }

    }
}