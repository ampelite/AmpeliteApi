using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class AuthDevices
    {
        public int AuthDId { get; set; }
        public int SEmpId { get; set; }
        public string AuthDMacAddress { get; set; }
        public string AuthDDevice { get; set; }

        public HrEmployee SEmp { get; set; }
    }
}
