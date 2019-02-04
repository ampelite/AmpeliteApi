using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using Microsoft.EntityFrameworkCore;

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

                var list = ctxAmpelite
                    .SpSaleproFrpLoyalty
                    .FromSql("sp_SALEPRO_FRPLoyalty @p0, @p1", parameters: new[] { month.ToString(), year.ToString() })
                    .ToList();

                if (list == null)
                    return NoContent();

                return Ok(list);

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
                    .FromSql("sp_SALEPRO_FRPLoyalty @p0, @p1", parameters: new[] { month.ToString(), year.ToString() })
                    .ToList();

                if (list == null)
                    return NoContent();

                return Ok(list);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private IEnumerable<IFrpLoyalty> Search(DateTime sDate, DateTime eDate)
        {

            var ampelTeam_ = ctxAmpelweb.AmpelTeam
                .Where(x => x.TeamCode.Equals(_teamCode))
                .Select(x => x.SaleCode)
                .Distinct().ToList();

            var goodCate_ = ctxAmpelite.GoodCateCode
                .Where(x => x.SubId == _subId && x.Status.Equals(_status))
                .Select(x => x.GoodCatecode)
                .Distinct().ToList();

            var pattn_ = new List<SaleproGoodPattn>(
                ctxAmpelite.SaleproGoodPattn
                .Where(x => x.Status.Equals(_status))
                .ToList());

            var frpCostRf_ = new List<SaleproFrpcostRf>(
                ctxAmpelite.SaleproFrpcostRf
                .Where(x => x.Status.EndsWith(_status))
                .ToList());


            var soLoyalty_ = (from tranSo in ctxAmpelite.GetTransactionSo

                              join pt in pattn_ on tranSo.GoodPattnCode equals pt.GoodPattnCode into a1
                              from gpattn in a1.DefaultIfEmpty()

                              where (tranSo.DocuDate.Date >= sDate && tranSo.DocuDate.Date <= eDate) &&
                              ampelTeam_.Contains(tranSo.EmpCode) &&
                              goodCate_.Contains(tranSo.ProductCode)

                              group tranSo by new
                              {
                                  tranSo.CustPono,
                                  tranSo.CustCode,
                                  tranSo.CustName,
                                  tranSo.ProductCode,
                                  tranSo.Product,
                                  tranSo.GoodPattnCode,
                                  gpattn.FactorCp,
                                  tranSo.GoodPrice2,
                                  tranSo.GoodPrice3,
                                  tranSo.EmpCode,
                                  tranSo.EmpNameEng
                              } into g

                              select new
                              {
                                  custPoNo = g.Key.CustPono,
                                  custCode = g.Key.CustCode,
                                  custName = g.Key.CustName.Trim(),
                                  goodCateCode = g.Key.ProductCode,
                                  goodCateName = g.Key.Product,
                                  goodPattnCode = g.Key.GoodPattnCode,
                                  goodPrice2 = g.Key.GoodPrice2,
                                  goodPrice3 = g.Key.GoodPrice3,
                                  empCode = g.Key.EmpCode,
                                  empName = g.Key.EmpNameEng,
                                  factorCp = g.Key.FactorCp,
                                  goodCompareQty = g.Sum(x => x.GoodCompareQty),
                                  goodQty2 = (g.Key.FactorCp == null) ? g.Sum(x => x.GoodQty2) : g.Sum(x => x.GoodQty2) * g.Key.FactorCp,
                                  goodAmnt = g.Sum(x => x.GoodAmnt),
                                  rf = g.Key.GoodPrice2 - ((g.Key.GoodPrice3 == null ? 0 : g.Key.GoodPrice3) / 100)
                              }).ToList();

            var finally_ = (from soLoyal in soLoyalty_

                            join costRf in frpCostRf_ on soLoyal.goodCateCode equals costRf.GoodCateCode into a1
                            from byCostRf in a1.DefaultIfEmpty()

                            group soLoyal by new
                            {
                                soLoyal.custCode,
                                soLoyal.custName,
                                soLoyal.empCode,
                                soLoyal.empName,
                                soLoyal.rf
                            } into g

                            select new IFrpLoyalty
                            {
                                CustCode = g.Key.custCode,
                                CustName = g.Key.custName,
                                EmpCode = g.Key.empCode,
                                EmpName = g.Key.empName,
                                Rf = g.Key.rf,
                                GoodQty2 = g.Sum(x => x.goodQty2),
                                GoodCompareQty = g.Sum(x => x.goodCompareQty),
                                GoodAmnt = g.Sum(x => x.goodAmnt)
                            }).ToList();
            return finally_;
        }

        // GET: api/LoyaltyFRP/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        public class IFrpLoyalty
        {
            public string CustCode { get; set; }
            public string CustName { get; set; }
            public string EmpCode { get; set; }
            public string EmpName { get; set; }
            public decimal? Rf { get; set; }
            public decimal? GoodQty2 { get; set; }
            public decimal GoodCompareQty { get; set; }
            public decimal GoodAmnt { get; set; }
        }
    }
}
