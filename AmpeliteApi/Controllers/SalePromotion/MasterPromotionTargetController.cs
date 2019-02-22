using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmpeliteApi.Data;
using AmpeliteApi.Models.Ampelite;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Route("api/SalePromotion/[controller]")]
    public class MasterPromotionTargetController : Controller
    {
        private db_AmpeliteContext ctx;
        public MasterPromotionTargetController(
            db_AmpeliteContext context
        )
        {
            ctx = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ListPromotionTarget> Get()
        {
            var list = (from tg in ctx.SaleProPromotionTargets
                        join pro in ctx.CodePromotion on tg.SubID equals pro.SubId
                        select new ListPromotionTarget
                        {
                            TargetID = tg.TargetID,
                            Target = tg.Target,
                            UnitDesc = tg.UnitDesc,
                            Description = tg.Description,
                            Reward = tg.Reward,
                            Discount = tg.Discount,
                            GiftVoucher = tg.GiftVoucher,
                            Bonus = tg.Bonus,
                            IsBonus = tg.IsBonus,
                            CostPromotion = tg.CostPromotion,
                            Status = tg.Status,
                            Month = tg.Month,
                            Year = tg.Year,
                            SubPromotion = pro.SubPromotion
                        });
            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class ListPromotionTarget
        {
            public int TargetID { get; set; }
            public decimal Target { get; set; }
            public string UnitDesc { get; set; }
            public string Description { get; set; }
            public decimal? Reward { get; set; }
            public decimal? Discount { get; set; }
            public decimal? GiftVoucher { get; set; }
            public decimal? Bonus { get; set; }
            public bool IsBonus { get; set; }
            public string CostPromotion { get; set; }
            public bool Status { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string SubPromotion { get; set; }
        }
    }
}
