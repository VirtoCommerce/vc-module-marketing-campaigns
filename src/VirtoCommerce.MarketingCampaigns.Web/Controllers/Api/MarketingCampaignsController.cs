using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.MarketingCampaigns.Core.Models.Search;
using VirtoCommerce.MarketingCampaigns.Core.Services;
using Permissions = VirtoCommerce.MarketingCampaigns.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.MarketingCampaigns.Web.Controllers.Api;

[Authorize]
[Route("api/marketing-campaigns")]
public class MarketingCampaignsController(
    IMarketingCampaignService campaignService,
    IMarketingCampaignSearchService campaignSearchService)
    : Controller
{
    /// <summary>
    /// Search marketing campaigns
    /// </summary>
    [HttpPost]
    [Route("search")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<MarketingCampaignSearchResult>> SearchCampaigns([FromBody] MarketingCampaignSearchCriteria criteria)
    {
        var result = await campaignSearchService.SearchAsync(criteria);
        return Ok(result);
    }

    /// <summary>
    /// Get marketing campaign by ID
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [Authorize(Permissions.Read)]
    public async Task<ActionResult<MarketingCampaign>> GetById(string id)
    {
        var campaigns = await campaignService.GetAsync([id]);
        var campaign = campaigns.Count > 0 ? campaigns[0] : null;
        return campaign != null ? Ok(campaign) : NotFound();
    }

    /// <summary>
    /// Create or update marketing campaign
    /// </summary>
    [HttpPost]
    [Route("")]
    [Authorize(Permissions.Create)]
    public async Task<ActionResult<MarketingCampaign>> SaveCampaign([FromBody] MarketingCampaign campaign)
    {
        await campaignService.SaveChangesAsync([campaign]);
        return Ok(campaign);
    }

    /// <summary>
    /// Delete marketing campaigns by IDs
    /// </summary>
    [HttpDelete]
    [Route("")]
    [Authorize(Permissions.Delete)]
    public async Task<ActionResult> DeleteCampaigns([FromQuery] IList<string> ids)
    {
        await campaignService.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// Attach promotions to campaign
    /// </summary>
    [HttpPost]
    [Route("{id}/promotions/attach")]
    [Authorize(Permissions.Update)]
    public async Task<ActionResult> AttachPromotions(string id, [FromBody] IList<string> promotionIds)
    {
        await campaignService.AttachPromotionsAsync(id, promotionIds);
        return NoContent();
    }

    /// <summary>
    /// Detach promotions from campaign
    /// </summary>
    [HttpPost]
    [Route("{id}/promotions/detach")]
    [Authorize(Permissions.Update)]
    public async Task<ActionResult> DetachPromotions(string id, [FromBody] IList<string> promotionIds)
    {
        await campaignService.DetachPromotionsAsync(id, promotionIds);
        return NoContent();
    }
}
