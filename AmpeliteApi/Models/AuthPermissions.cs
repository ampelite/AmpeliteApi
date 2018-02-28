using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class AuthPermissions
    {
        public int AuthPId { get; set; }
        public int SEmpId { get; set; }
        public string AuthPRole { get; set; }
        public bool AuthPStatus { get; set; }
        public DateTime AuthPUpdateAt { get; set; }
        public string AuthPUpdateBy { get; set; }

        public HrEmployee SEmp { get; set; }
    }
}
