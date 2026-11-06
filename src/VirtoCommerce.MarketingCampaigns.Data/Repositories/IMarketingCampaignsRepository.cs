using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.MarketingCampaigns.Data.Models;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.MarketingCampaigns.Data.Repositories;

public interface IMarketingCampaignsRepository : IRepository
{
    IQueryable<MarketingCampaignEntity> MarketingCampaigns { get; }
    IQueryable<MarketingCampaignPromotionEntity> MarketingCampaignPromotions { get; }

    Task<IList<MarketingCampaignEntity>> GetMarketingCampaignsByIdsAsync(IList<string> ids);
}
