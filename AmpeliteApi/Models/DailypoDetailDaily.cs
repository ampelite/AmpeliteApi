using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Models
{
    public class DailypoDetailDaily
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
        //public string GroupBrandCode { get; set; }
        //public string GroupBrandName { get; set; }
        //public string GoodBrandCode { get; set; }
        //public string GoodBrandName { get; set; }
        //public string ProductCode { get; set; }
        //public string Product { get; set; }
        public string GoodName { get; set; }
        public decimal GoodQty { get; set; }
        public decimal GoodPrice2 { get; set; }
        public decimal GoodAmnt { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
    }
}
