using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.MarketingCampaigns.Data.Models;

public class MarketingCampaignPromotionEntity : Entity
{
    [Required]
    [StringLength(128)]
    public string CampaignId { get; set; }

    public virtual MarketingCampaignEntity Campaign { get; set; }

    [Required]
    [StringLength(128)]
    public string PromotionId { get; set; }
}
