using System;
using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.MarketingCampaigns.Core.Models;

public class MarketingCampaign : AuditableEntity, ICloneable
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string StoreId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IList<string> PromotionIds { get; set; } = new List<string>();

    public object Clone()
    {
        var result = (MarketingCampaign)MemberwiseClone();
        result.PromotionIds = PromotionIds != null ? new List<string>(PromotionIds) : null;
        return result;
    }
}
