using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class DailypoProductTeam
    {
        public int GptId { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string ReCateProduct { get; set; }
        public bool? IsActive { get; set; }
    }
}
