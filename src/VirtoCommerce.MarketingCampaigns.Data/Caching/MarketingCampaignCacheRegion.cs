using System;
using VirtoCommerce.Platform.Core.Caching;

namespace VirtoCommerce.MarketingCampaigns.Data.Caching;

public class MarketingCampaignCacheRegion : CancellableCacheRegion<MarketingCampaignCacheRegion>
{
    public static TimeSpan DefaultExpirationTime { get; set; } = TimeSpan.FromMinutes(5);
}
