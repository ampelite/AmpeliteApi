using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class GetTransactionInv
    {
        public string Id { get; set; }
        public DateTime DocuDate { get; set; }
        public string DocuNo { get; set; }
        public string InvNo { get; set; }
        public int? GoodCateId { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string GoodId { get; set; }
        public string GoodCode { get; set; }
        public string Model { get; set; }
        public int? MainGoodUnitId { get; set; }
        public string GoodUnitName { get; set; }
        public int? GoodBrandId { get; set; }
        public string GoodBrandCode { get; set; }
        public string GoodBrandName { get; set; }
        public int? GoodGroupId { get; set; }
        public string GoodGroupCode { get; set; }
        public string GoodGroupName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RentAmount { get; set; }
        public decimal? TotalPrice { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string EmpCode { get; set; }
        public string SaleName { get; set; }
        public string SaleAreaCode { get; set; }
        public string SaleAreaName { get; set; }
        public int? BrchId { get; set; }
        public string DocuStatus { get; set; }
        public int? Docutype { get; set; }
        public decimal? GoodPrice2 { get; set; }
        public decimal? GoodPrice3 { get; set; }
        public int? GoodPattnId { get; set; }
        public string GoodPattnCode { get; set; }
        public string GoodPattnName { get; set; }
        public int? GoodColorId { get; set; }
        public string GoodColorCode { get; set; }
        public string GoodColorName { get; set; }
        public string CustTypeCode { get; set; }
        public string CustTypeName { get; set; }
        public string Remark { get; set; }
        public string GoodClassCode { get; set; }
        public string GoodClassName { get; set; }
        public int? GoodTypeId { get; set; }
        public string GoodTypeCode { get; set; }
        public string GoodTypeName { get; set; }
        public decimal? BillDiscAmnt { get; set; }
        public string CustPono { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string GoodName { get; set; }
        public string DataFrom { get; set; }
        public string GoodColorNameEng { get; set; }
        public decimal? GoodPrice1 { get; set; }
        public string GoodMarketName { get; set; }
    }
}
