using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class AuthTransactions
    {
        public int AuthTId { get; set; }
        public int SEmpId { get; set; }
        public string AuthDMacAddress { get; set; }
        public DateTime AuthTLastLoginDate { get; set; }

        public HrEmployee SEmp { get; set; }
    }
}
