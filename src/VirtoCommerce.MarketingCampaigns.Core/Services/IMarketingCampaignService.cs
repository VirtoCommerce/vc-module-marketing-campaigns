using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.Platform.Core.GenericCrud;

namespace VirtoCommerce.MarketingCampaigns.Core.Services;

public interface IMarketingCampaignService : ICrudService<MarketingCampaign>
{
    Task AttachPromotionsAsync(string campaignId, IList<string> promotionIds);
    Task DetachPromotionsAsync(string campaignId, IList<string> promotionIds);
}
