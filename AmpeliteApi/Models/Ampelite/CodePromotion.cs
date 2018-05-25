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
    }
}
