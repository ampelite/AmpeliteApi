using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Models.Ampelweb
{
    public class AmpelTeam
    {
        public int Id { get; set; }
        public string SaleCode { get; set; }
        public string SaleName { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string ZoneGroupCode { get; set; }
        public string ZoneGroup { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public string TeamGroupCode { get; set; }
        public string TeamGroup { get; set; }
        public decimal? Traget { get; set; }
        public string AddSaleTotal { get; set; }
        public string SaleCodeRpt { get; set; }
        public string SaleNameRpt { get; set; }
        public DateTime? UpdateNow { get; set; }
        public string UserUpdate { get; set; }

    }
}
