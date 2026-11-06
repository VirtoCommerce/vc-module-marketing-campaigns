using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.MarketingCampaigns.Core.Events;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.MarketingCampaigns.Core.Services;
using VirtoCommerce.MarketingCampaigns.Data.Models;
using VirtoCommerce.MarketingCampaigns.Data.Repositories;
using VirtoCommerce.MarketingModule.Core.Services;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.MarketingCampaigns.Data.Services;

public class MarketingCampaignService(
    Func<IMarketingCampaignsRepository> repositoryFactory,
    IPlatformMemoryCache platformMemoryCache,
    IEventPublisher eventPublisher,
    IPromotionService promotionService)
    : CrudService<MarketingCampaign, MarketingCampaignEntity, MarketingCampaignChangingEvent,
            MarketingCampaignChangedEvent>(repositoryFactory, platformMemoryCache, eventPublisher),
        IMarketingCampaignService
{
    private readonly Func<IMarketingCampaignsRepository> _repositoryFactory = repositoryFactory;

    public async Task AttachPromotionsAsync(string campaignId, IList<string> promotionIds)
    {
        var campaigns = await GetAsync([campaignId]);
        var campaign = campaigns.FirstOrDefault() ?? throw new InvalidOperationException($"Campaign '{campaignId}' not found.");

        var newIds = promotionIds.Except(campaign.PromotionIds).ToList();
        if (newIds.Count == 0) return;

        campaign.PromotionIds = campaign.PromotionIds.Concat(newIds).ToList();
        await SaveChangesAsync([campaign]);

        await PropagateToPromotionsAsync(campaign, newIds);
    }

    public async Task DetachPromotionsAsync(string campaignId, IList<string> promotionIds)
    {
        var campaigns = await GetAsync([campaignId]);
        var campaign = campaigns.FirstOrDefault() ?? throw new InvalidOperationException($"Campaign '{campaignId}' not found.");

        campaign.PromotionIds = campaign.PromotionIds.Except(promotionIds).ToList();
        await SaveChangesAsync([campaign]);
    }

    public override async Task SaveChangesAsync(IList<MarketingCampaign> campaigns)
    {
        var modifiedCampaigns = campaigns.Where(c => !c.IsTransient()).ToList();
        var existingMap = new Dictionary<string, MarketingCampaign>();

        if (modifiedCampaigns.Count > 0)
        {
            var existing = await GetAsync(modifiedCampaigns.Select(c => c.Id).ToList());
            existingMap = existing.ToDictionary(c => c.Id);
        }

        await base.SaveChangesAsync(campaigns);

        foreach (var campaign in modifiedCampaigns)
        {
            if (!existingMap.TryGetValue(campaign.Id, out var old)) continue;

            var hasRelevantChange = old.StoreId != campaign.StoreId
                || old.IsActive != campaign.IsActive
                || old.StartDate != campaign.StartDate
                || old.EndDate != campaign.EndDate;

            if (hasRelevantChange && campaign.PromotionIds?.Count > 0)
            {
                await PropagateToPromotionsAsync(campaign, campaign.PromotionIds);
            }
        }
    }

    protected override async Task<IList<MarketingCampaignEntity>> LoadEntities(IRepository repository, IList<string> ids, string responseGroup)
    {
        return await ((IMarketingCampaignsRepository)repository).GetMarketingCampaignsByIdsAsync(ids);
    }

    private async Task PropagateToPromotionsAsync(MarketingCampaign campaign, IList<string> promotionIds)
    {
        if (promotionIds == null || promotionIds.Count == 0) return;

        var promotions = await promotionService.GetAsync(promotionIds.ToList());
        if (promotions == null || promotions.Count == 0) return;

        foreach (var promotion in promotions)
        {
            promotion.IsActive = campaign.IsActive;
            promotion.StartDate = campaign.StartDate;
            promotion.EndDate = campaign.EndDate;

            if (!string.IsNullOrEmpty(campaign.StoreId))
            {
                promotion.StoreIds = [campaign.StoreId];
            }
        }

        await promotionService.SaveChangesAsync(promotions);
    }
}
