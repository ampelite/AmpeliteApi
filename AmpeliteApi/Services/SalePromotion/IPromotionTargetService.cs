using System;
using System.Collections.Generic;
using AmpeliteApi.Models;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface IPromotionTargetService
    {
        List<DropDowns> UnitDropDowns();
    }
}
