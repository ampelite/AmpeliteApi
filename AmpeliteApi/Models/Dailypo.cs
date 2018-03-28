using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Models.Dailypo
{
    //public class Dailypo
    //{
    //}

    public class DailyDetail
    {
        public Int64 ID { get; set; }
        public DateTime DocuDate { get; set; }
        public string DocuNo { get; set; }
        public string CustPONo { get; set; }
        public string JobName { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string CustomerMainCode { get; set; }
        public string CustomerName { get; set; }
        public string GroupBrandCode { get; set; }
        public string GroupBrandName { get; set; }
        public string GoodBrandCode { get; set; }
        public string GoodBrandName { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string GoodName { get; set; }
        public decimal GoodQty { get; set; }
        public decimal GoodPrice2 { get; set; }
        public decimal GoodAmnt { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
    }

    public class DailyProduct
    {
        public Int64 ID { get; set; }
        public DateTime EDate { get; set; }
        public Int32 WeekDay { get; set; }
        public Int32 Day { get; set; }
        public string Type { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public decimal? Unit { get; set; }
    }

    public class DailyGroupReport
    {
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
    }

    public class DailyGroupUnit
    {
        public int UnitId { get; set; }
        public string GroupCode { get; set; }
        public string UnitCode { get; set; }
        public decimal? UnitValue { get; set; }
        public string UnitName { get; set; }
        public string UnitTitle { get; set; }
    }

    public class DailyProductTeam
    {
        public int GptId { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string ReCateProduct { get; set; }
        public bool? IsActive { get; set; }
    }

    public class DailyProductGroup
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string GoodBrandCode { get; set; }
        public string GoodBrandName { get; set; }
        public string GroupBrandCode { get; set; }
        public string GroupBrandName { get; set; }
        public string ProductType { get; set; }
    }

}
