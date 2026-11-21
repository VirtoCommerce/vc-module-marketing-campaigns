using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.MarketingCampaigns.Core.Models.Search;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace VirtoCommerce.MarketingCampaigns.Core.Services;

public interface IMarketingCampaignSearchService : ISearchService<MarketingCampaignSearchCriteria, MarketingCampaignSearchResult, MarketingCampaign>
{
}
