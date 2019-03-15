using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using AmpeliteApi.Models.Ampelite;
using AmpeliteApi.Services.SalePromotion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Route("api/SalePromotion/[controller]")]
    public class MasterPromotionTargetController : Controller
    {
        private db_AmpeliteContext ctx;
        private IPromotionTargetService iProTargetService;
        private ICodePromotionService iCodeProService;

        public MasterPromotionTargetController(
            db_AmpeliteContext context,
            IPromotionTargetService iPromotionTargetService,
            ICodePromotionService iCodePromotionService
        )
        {
            ctx = context;
            iProTargetService = iPromotionTargetService;
            iCodeProService = iCodePromotionService;
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
        [HttpGet("GetByCon")]
        public IActionResult GetByCon(int? targetId)
        {
            try
            {
                var unitDropDowns = iProTargetService.UnitDropDowns();
                var subPromotionDropDowns = iCodeProService.SubPromotionDropDowns();
                var proTarget = ctx.SaleProPromotionTargets
                    .FirstOrDefault(x => x.TargetID == targetId);

                var disabledEdit = ctx.SaleProBalanceDTs
                    .Where(x => x.TargetID == targetId)
                    .Any();

                var response = new PromotionTargetResponse
                {
                    SubPromotionDropDowns = subPromotionDropDowns,
                    UnitDropDowns = unitDropDowns,
                    PromotionTarget = proTarget,
                    DisabledEdit = disabledEdit
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
           
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SaleProPromotionTarget tg)
        {
            try
            {
                tg.CreateDate = DateTime.Now;
                ctx.SaleProPromotionTargets.Add(tg);
                ctx.SaveChanges();

                var obj = new Dictionary<string, object> {
                    {"targetID", tg.TargetID}
                };
                return Ok(obj);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]SaleProPromotionTarget tg)
        {
            tg.UpdateDate = DateTime.Now;

            if (!SaleProPromotionTargetExists(tg.TargetID))
                return NotFound();

            ctx.Entry(tg).State = EntityState.Modified;

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool SaleProPromotionTargetExists(int targetId)
        {
            return ctx.SaleProPromotionTargets.Any(e => e.TargetID == targetId);
        }

        public class PromotionTargetResponse
        {
            public List<DropDowns> SubPromotionDropDowns { get; set; }
            public List<DropDowns> UnitDropDowns { get; set; }
            public SaleProPromotionTarget PromotionTarget { get; set; }
            public bool DisabledEdit { get; set; }
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
