using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class GetTransactionSo
    {
        public string Id { get; set; }
        public int Soid { get; set; }
        public DateTime DocuDate { get; set; }
        public string DocuNo { get; set; }
        public string CustPono { get; set; }
        public int CustId { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public int? GoodCateId { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public int? GoodBrandId { get; set; }
        public string GoodBrandCode { get; set; }
        public string GoodBrandName { get; set; }
        public int? GoodClassId { get; set; }
        public string GoodClassCode { get; set; }
        public string GoodClassName { get; set; }
        public int? GoodPattnId { get; set; }
        public string GoodPattnCode { get; set; }
        public string GoodPattnName { get; set; }
        public int? GoodColorId { get; set; }
        public string GoodColorCode { get; set; }
        public string GoodColorName { get; set; }
        public string GoodColorNameEng { get; set; }
        public int? GoodTypeId { get; set; }
        public string GoodTypeCode { get; set; }
        public string GoodTypeName { get; set; }
        public int ListNo { get; set; }
        public int? GoodId { get; set; }
        public string GoodCode { get; set; }
        public string GoodName { get; set; }
        public decimal GoodStockQty { get; set; }
        public decimal GoodQty2 { get; set; }
        public decimal GoodCompareQty { get; set; }
        public decimal GoodPrice2 { get; set; }
        public decimal? GoodPrice3 { get; set; }
        public int? GoodUnitId2 { get; set; }
        public string GoodUnitName { get; set; }
        public int? BrchId { get; set; }
        public decimal GoodAmnt { get; set; }
        public decimal? SumGoodAmnt { get; set; }
        public string BillDiscFormula { get; set; }
        public decimal? BillDiscAmnt { get; set; }
        public decimal? TotaBaseAmnt { get; set; }
        public decimal? BillAftrDiscAmnt { get; set; }
        public int? VatRate { get; set; }
        public decimal? Vatamnt { get; set; }
        public decimal? NetAmnt { get; set; }
        public decimal? GoodDiscFormula { get; set; }
        public decimal? GoodDiscAmnt { get; set; }
        public int? DocuType { get; set; }
        public int? SaleAreaId { get; set; }
        public string SaleAreaCode { get; set; }
        public string SaleAreaName { get; set; }
        public int? DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int? EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string EmpNameEng { get; set; }
        public string ClearSo { get; set; }
        public string OnHold { get; set; }
        public int? JobId { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string JobNameEng { get; set; }
        public int? InveId { get; set; }
        public string InveCode { get; set; }
        public string InveName { get; set; }
        public string InveNameEng { get; set; }
        public string DataFrom { get; set; }
        public int? TranspId { get; set; }
        public string TranspName { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ShipToAddr1 { get; set; }
    }
}
