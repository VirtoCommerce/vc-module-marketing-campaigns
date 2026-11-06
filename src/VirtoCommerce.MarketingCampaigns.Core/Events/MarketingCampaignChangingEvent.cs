using System.Collections.Generic;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.MarketingCampaigns.Core.Events;

public class MarketingCampaignChangingEvent(IEnumerable<GenericChangedEntry<MarketingCampaign>> changedEntries)
    : GenericChangedEntryEvent<MarketingCampaign>(changedEntries);
