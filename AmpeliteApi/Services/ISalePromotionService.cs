using System;
using System.Collections.Generic;
using AmpeliteApi.Models;
namespace AmpeliteApi.Services
{
    public interface ISalePromotionService
    {

        List<DropDowns> SubPromotionDropDowns { get; set; }
    }
}
