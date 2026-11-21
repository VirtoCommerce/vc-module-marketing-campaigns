using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.MarketingCampaigns.Core.Models.Search;
using VirtoCommerce.MarketingCampaigns.Core.Services;
using VirtoCommerce.MarketingCampaigns.Data.Models;
using VirtoCommerce.MarketingCampaigns.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.MarketingCampaigns.Data.Services;

public class MarketingCampaignSearchService(
    Func<IMarketingCampaignsRepository> repositoryFactory,
    IPlatformMemoryCache platformMemoryCache,
    IMarketingCampaignService crudService,
    IOptions<CrudOptions> crudOptions)
    : SearchService<MarketingCampaignSearchCriteria, MarketingCampaignSearchResult, MarketingCampaign,
            MarketingCampaignEntity>(repositoryFactory, platformMemoryCache, crudService, crudOptions),
        IMarketingCampaignSearchService
{
    protected override IQueryable<MarketingCampaignEntity> BuildQuery(IRepository repository, MarketingCampaignSearchCriteria criteria)
    {
        var query = ((IMarketingCampaignsRepository)repository).MarketingCampaigns;

        if (!criteria.Keyword.IsNullOrEmpty())
        {
            query = query.Where(x => x.Name.Contains(criteria.Keyword) || x.Code.Contains(criteria.Keyword));
        }

        if (!criteria.StoreId.IsNullOrEmpty())
        {
            query = query.Where(x => x.StoreId == criteria.StoreId);
        }

        if (criteria.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == criteria.IsActive.Value);
        }

        return query;
    }

    protected override IList<SortInfo> BuildSortExpression(MarketingCampaignSearchCriteria criteria)
    {
        var sortInfos = criteria.SortInfos;

        if (sortInfos.IsNullOrEmpty())
        {
            sortInfos =
            [
                new SortInfo
                {
                    SortColumn = nameof(MarketingCampaign.CreatedDate),
                    SortDirection = SortDirection.Descending,
                },
            ];
        }

        return sortInfos;
    }
}
