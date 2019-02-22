using System;
using System.Collections.Generic;
using AmpeliteApi.Models;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface ICodePromotionService
    {
        List<DropDowns> MainPromotionDropDowns();

        List<DropDowns> SubPromotionDropDowns();

        List<DropDowns> SubPromotionWithMainProDropDowns(string mainPro);
    }
}
