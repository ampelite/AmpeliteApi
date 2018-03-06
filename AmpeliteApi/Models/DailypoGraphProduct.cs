using System;

namespace AmpeliteApi.Models
{
    public class DailypoGraphProduct
    {
        public Int64 ID { get; set; }
        public DateTime EDate { get; set; }
        public Int32 WeekDay { get; set; }
        public Int32 Day { get; set; }
        public String Type { get; set; }
        public String TeamCode { get; set; }
        public String TeamName { get; set; }
        public Decimal Unit { get; set; }
    }
}