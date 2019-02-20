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
        private readonly string _subId = "0006";

        public LoyaltyFRPController(db_AmpeliteContext context1, db_AmpelwebContext context2)
        {
            ctxAmpelite = context1;
            ctxAmpelweb = context2;
        }

        // GET: api/LoyaltyFRP
        [HttpGet("GetPromotionTargets")]
        public IEnumerable<SaleProPromotionTarget> GetPromotionTargets(int month, int year)
        {
            var target = ctxAmpelite
                .SaleProPromotionTargets
                .Where(x => x.SubID == _subId && x.Month == month && x.Year == year)
                .ToList();

            return target;
        }


        [HttpGet("GetByCon")]
        public IActionResult GetByCon(int month, int year)
        {
            try
            {
                var list = ctxAmpelite
                    .SpSaleproFrpLoyalty
                    .FromSql("sp_SALEPRO_FRPLoyalty @p0, @p1", new[] { month.ToString(), year.ToString() }).ToList();

                if (!list.Any())
                    return NoContent();

                var _hd = ctxAmpelite.SaleProBalanceHDs
                    .Where(hd => hd.Month == month && hd.Year == year)
                    .Include(hd => hd.BalancesDT)
                    .ToList();

                var target = ctxAmpelite.SaleProPromotionTargets
                        .Where(x => x.SubID == _subId && x.Month == month && x.Year == year)
                        .Select(x => new SaleProPromotionTarget
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

                if (!target.Any())
                    return NoContent();

                var frpLoyalty = new List<SaleProBalanceHD>();

                frpLoyalty = (from sp in list
                              join hd in _hd on sp.BHDID equals hd.BHDID into hds
                              from h in hds.DefaultIfEmpty()

                              select new SaleProBalanceHD
                              {
                                  BHDID = sp.BHDID,
                                  SUBID = _subId,
                                  IsConfirm = sp.IsConfirm,
                                  CustCode = sp.CustCode,
                                  CustName = sp.CustName,
                                  EmpCode = sp.EmpCode,
                                  EmpName = sp.EmpName,
                                  Month = month,
                                  Year = year,
                                  GoodQty2 = sp.GoodQty2,
                                  GoodAmnt = sp.GoodAmnt,
                                  BalancesDT = h == null ? new List<SaleProBalanceDT>() : h.BalancesDT
                              }).ToList();

                frpLoyalty.ForEach(x =>
                {
                    var dtList = new List<SaleProBalanceDT>();
                    target.ForEach(tg =>
                    {
                        var dt = x.BalancesDT.FirstOrDefault(_dt => _dt.TargetID == tg.TargetID);

                        if (dt == null) dt = new SaleProBalanceDT();

                        dt.BHDID = x.BHDID;
                        dt.BDTID = false ? 0 : dt.BDTID;
                        dt.TargetID = tg.TargetID;
                        dt.Target = tg.Target;
                        dt.IsBonus = tg.IsBonus;
                        dt.Unit = tg.Unit;
                        dt.BalanceHD = null;

                        if (tg.Unit == 1 && x.GoodQty2 >= tg.Target)
                        {
                            dt.Reward = tg.Reward;
                            dt.GiftVoucher = tg.GiftVoucher;
                            dt.Discount = tg.Discount;
                            dt.RewardSelect = dt.BHDID == 0 || dt.RewardSelect;
                            dt.GiftSelect = dt.BHDID == 0 || dt.GiftSelect;
                            dt.DiscountSelect = dt.BHDID == 0 || dt.DiscountSelect;

                        } 
                        else if (tg.Unit == 2 && x.GoodAmnt >= tg.Target)
                        {
                            dt.Bonus = tg.Bonus;
                            dt.BonusSelect = dt.BHDID == 0 || dt.BonusSelect;
                        }

                        dtList.Add(dt);
                    });
                    x.BalancesDT = dtList;
                });


                var responst = new FrpLoyaltyResponse{
                    SaleProBalanceHDs = frpLoyalty,
                    SaleProPromotionTargets = GetPromotionTargets(month, year).ToList()
                };

                return Ok(responst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        public class FrpLoyaltyResponse
        {
            public List<SaleProBalanceHD> SaleProBalanceHDs { get; set; }
            public List<SaleProPromotionTarget> SaleProPromotionTargets { get; set; }
        }
    }
}
