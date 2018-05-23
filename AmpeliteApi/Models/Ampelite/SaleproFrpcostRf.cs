using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class SaleproFrpcostRf
    {
        public int Id { get; set; }
        public string GoodCateCode { get; set; }
        public string GoodCateName { get; set; }
        public decimal Amount { get; set; }
        public decimal Rf { get; set; }
        public decimal TrsP { get; set; }
        public string SubId { get; set; }
        public string SubCodePro { get; set; }
        public decimal MaxLength { get; set; }
        public string Status { get; set; }
    }
}
