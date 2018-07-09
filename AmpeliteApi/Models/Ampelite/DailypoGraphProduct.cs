using System;

namespace AmpeliteApi.Models
{
    public class DailypoGraphProduct
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
}