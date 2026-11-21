using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.MarketingCampaigns.Core.Models.Search;

public class MarketingCampaignSearchCriteria : SearchCriteriaBase
{
    public string StoreId { get; set; }
    public bool? IsActive { get; set; }
}
