using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class CodePromotion
    {
        public string SubId { get; set; }
        public string SubCodePro { get; set; }
        public string SubPromotion { get; set; }
        public int? CodeMainPro { get; set; }
        public string MainPro { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpateBy { get; set; }
        public DateTime? UpateDate { get; set; }
    }
}
