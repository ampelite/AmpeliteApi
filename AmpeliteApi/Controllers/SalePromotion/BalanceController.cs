using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Data;
using AmpeliteApi.Models.Ampelite;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/SalePromotion/[controller]")]
    public class BalanceController : Controller
    {
        private db_AmpeliteContext ctx;
        public BalanceController(
            db_AmpeliteContext context
        )
        {
            ctx = context;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Balance value)
        {
            using(var trans = ctx.Database.BeginTransaction())
            {
                try
                {
                    var Exists = value.BalancesHD.Where(x => x.BHDID > 0).ToList();
                    if (Exists.Any())
                    {
                        Exists.ForEach(o =>
                        {
                            var hd = ctx.SaleProBalanceHDs.First(x => x.BHDID == o.BHDID);
                            hd.IsConfirm = o.IsConfirm;
                            hd.CustCode = o.CustCode;
                            hd.CustName = o.CustName;
                            hd.EmpCode = o.EmpCode;
                            hd.EmpName = o.EmpName;
                            hd.GoodQty2 = o.GoodQty2;
                            hd.GoodAmnt = o.GoodAmnt;
                            hd.TotalReward = o.TotalReward;
                            hd.TotalGiftVoucher = o.TotalGiftVoucher;
                            hd.TotalDiscount = o.TotalDiscount;
                            hd.TotalBonus = o.TotalBonus;
                            ctx.SaveChanges();

                            o.BalancesDT.ToList().ForEach(p =>
                            {
                                var dt = ctx.SaleProBalanceDTs.First(x => x.BDTID == p.BDTID);
                                dt.Target = p.Target;
                                dt.Reward = p.Reward;
                                dt.GiftVoucher = p.GiftVoucher;
                                dt.Discount = p.Discount;
                                dt.Bonus = p.Bonus;
                                dt.IsBonus = p.IsBonus;
                                dt.Unit = p.Unit;
                                dt.RewardSelect = p.RewardSelect;
                                dt.GiftSelect = p.GiftSelect;
                                dt.DiscountSelect = p.DiscountSelect;
                                dt.BonusSelect = p.BonusSelect;

                                if (p.BDTID == 0) 
                                    ctx.SaleProBalanceDTs.Add(p);

                                ctx.SaveChanges();
                            });
                        });
                    }

                    var NotExists = value.BalancesHD.Where(x => x.BHDID == 0);
                    if (NotExists.Any())
                    {
                        ctx.AddRange(NotExists);
                        ctx.SaveChanges();
                    }

                    trans.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return StatusCode(500, ex);
                }
            }
        }

        public class Balance
        {
            public List<SaleProBalanceHD> BalancesHD { get; set; }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Balance value)
        {
            try
            {
                var Exists = value.BalancesHD.Where(x => x.BHDID > 0).ToList();
                if (Exists.Any())
                {
                    Exists.ForEach(o =>
                    {
                        var hd = ctx.SaleProBalanceHDs.First(x => x.BHDID == o.BHDID);
                        hd.IsConfirm = o.IsConfirm;
                        hd.CustCode = o.CustCode;
                        hd.CustName = o.CustName;
                        hd.EmpCode = o.EmpCode;
                        hd.EmpName = o.EmpName;
                        hd.GoodQty2 = o.GoodQty2;
                        hd.GoodAmnt = o.GoodAmnt;
                        hd.TotalReward = o.TotalReward;
                        hd.TotalGiftVoucher = o.TotalGiftVoucher;
                        hd.TotalDiscount = o.TotalDiscount;
                        hd.TotalBonus = o.TotalBonus;
                        ctx.SaveChanges();

                        o.BalancesDT.ToList().ForEach(p => 
                        {
                            var dt = ctx.SaleProBalanceDTs.First(x => x.BDTID == p.BDTID);
                            dt.Target = p.Target;
                            dt.Reward = p.Reward;
                            dt.GiftVoucher = p.GiftVoucher;
                            dt.Discount = p.Discount;
                            dt.Bonus = p.Bonus;
                            dt.IsBonus = p.IsBonus;
                            dt.Unit = p.Unit;
                            dt.RewardSelect = p.RewardSelect;
                            dt.GiftSelect = p.GiftSelect;
                            dt.DiscountSelect = p.DiscountSelect;
                            dt.BonusSelect = p.BonusSelect;

                            ctx.SaveChanges();
                        });
                    });
                }

                var NotExists = value.BalancesHD.Where(x => x.BHDID == 0);
                if (NotExists.Any())
                {
                    ctx.AddRangeAsync(NotExists);
                    ctx.SaveChangesAsync();
                }

                return Ok();
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
