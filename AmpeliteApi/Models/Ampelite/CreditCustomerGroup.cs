using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class CreditCustomerGroup
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMainCode { get; set; }
        public string CustomerHardCode { get; set; }
        public bool? MainGroup { get; set; }
        public string Collateral { get; set; }
        public string CollateralPersonal { get; set; }
        public decimal? ColAmount { get; set; }
        public DateTime? ColDay { get; set; }
        public string Collateral2 { get; set; }
        public string CollateralPersonal2 { get; set; }
        public decimal? ColAmount2 { get; set; }
        public DateTime? ColDay2 { get; set; }
        public string Collateral3 { get; set; }
        public string CollateralPersonal3 { get; set; }
        public decimal? ColAmount3 { get; set; }
        public DateTime? ColDay3 { get; set; }
        public string Collateral4 { get; set; }
        public string CollateralPersonal4 { get; set; }
        public decimal? ColAmount4 { get; set; }
        public DateTime? ColDay4 { get; set; }
        public int? CreditTerm1Frpday { get; set; }
        public bool? Pdcterm1FrpdayType { get; set; }
        public int? CreditTerm1ScrewDay { get; set; }
        public bool? Pdcterm1ScrewDayType { get; set; }
        public decimal? Pdcamount { get; set; }
        public int? Pdcday { get; set; }
        public bool? TypePdc { get; set; }
        public decimal? TempAmount { get; set; }
        public int? TempDay { get; set; }
        public bool? TempTypePdc { get; set; }
        public bool? WinspeedTypePdc { get; set; }
        public string ConditionRemark { get; set; }
        public string CreditStatusName { get; set; }
        public bool? CreditStatusActive { get; set; }
        public string CustomerType { get; set; }
        public string CustCodeId { get; set; }
    }
}
