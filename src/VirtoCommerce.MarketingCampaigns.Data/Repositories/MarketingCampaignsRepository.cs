using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.MarketingCampaigns.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.MarketingCampaigns.Data.Repositories;

public class MarketingCampaignsRepository(MarketingCampaignsDbContext dbContext)
    : DbContextRepositoryBase<MarketingCampaignsDbContext>(dbContext), IMarketingCampaignsRepository
{
    public IQueryable<MarketingCampaignEntity> MarketingCampaigns => DbContext.Set<MarketingCampaignEntity>();
    public IQueryable<MarketingCampaignPromotionEntity> MarketingCampaignPromotions => DbContext.Set<MarketingCampaignPromotionEntity>();

    public async Task<IList<MarketingCampaignEntity>> GetMarketingCampaignsByIdsAsync(IList<string> ids)
    {
        return await MarketingCampaigns
            .Include(x => x.Promotions)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}
