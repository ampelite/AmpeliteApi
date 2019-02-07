using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Models.Ampelite;

namespace AmpeliteApi.Controllers.SalePromotion
{
    [Produces("application/json")]
    [Route("api/SalePromotion/LoyaltyFRP")]
    public class LoyaltyFRPController : Controller
    {
        private readonly db_AmpeliteContext ctxAmpelite;
        private readonly db_AmpelwebContext ctxAmpelweb;
        private readonly string _teamCode = "T001";
        private readonly string _subId = "0006";
        private readonly string _status = "Y";

        public LoyaltyFRPController(db_AmpeliteContext context1, db_AmpelwebContext context2)
        {
            ctxAmpelite = context1;
            ctxAmpelweb = context2;
        }

        // GET: api/LoyaltyFRP
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dateNow = DateTime.Now.Date;
                var month = dateNow.Month;
                var year = dateNow.Year;

                var target = ctxAmpelite.SaleProPromotionTargets.Where(x => x.SubID == _subId).ToList();

                return Ok(target);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByCon")]
        public IActionResult GetByCon(int month, int year)
        {
            try
            {

                var list = ctxAmpelite
                    .SpSaleproFrpLoyalty
                    .FromSql("sp_SALEPRO_FRPLoyalty @p0, @p1", new[] { month.ToString(), year.ToString() })
                    .Select(x => new SaleProBalanceHD
                    {
                        CustCode = x.CustCode,
                        CustName = x.CustName,
                        EmpCode = x.EmpCode,
                        EmpName = x.EmpName,
                        GoodQty2 = x.GoodQty2,
                        GoodAmnt = x.GoodAmnt
                    }).ToList();

                if (list == null)
                    return NoContent();

                var rewards = ctxAmpelite.SaleProPromotionTargets
                .Where(x => x.SubID == _subId)
                .Select(x => new SaleProBalanceDT
                {
                    TargetID = x.TargetID,
                    Target = x.Target,
                    Reward = x.Reward,
                    GiftVoucher = x.GiftVoucher,
                    Discount = x.Discount,
                    Bonus = x.Bonus,
                    IsBonus = x.IsBonus,
                    Unit = x.Unit
                }).ToList();

                var frpLoyalty = new List<SaleProBalanceHD>();

                list.ForEach(frp =>
                {
                    
                    var dt = new List<SaleProBalanceDT>();
                    rewards.ForEach(rew =>
                    {
                        if (rew.Unit == 1 && frp.GoodQty2 >= rew.Target)
                        {
                            dt.Add(rew);
                        } 
                        else if (rew.Unit == 2 && frp.GoodAmnt >= rew.Target)
                        {
                            dt.Add(rew);
                        }
                        else                     
                        {
                            var _dt = new SaleProBalanceDT
                            {
                                IsBonus = rew.IsBonus
                            };
                            dt.Add(_dt);
                        }
                    });

                    var _frp = new SaleProBalanceHD
                    {
                        CustCode = frp.CustCode,
                        CustName = frp.CustName,
                        EmpCode = frp.EmpCode,
                        EmpName = frp.EmpName,
                        GoodQty2 = frp.GoodQty2,
                        GoodAmnt = frp.GoodAmnt,
                        BalancesDT = dt
                    };

                    frpLoyalty.Add(_frp);
                });

                return Ok(frpLoyalty);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
         }

    }
}
