using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VirtoCommerce.MarketingCampaigns.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace VirtoCommerce.MarketingCampaigns.Data.Models;

public class MarketingCampaignEntity : AuditableEntity, IDataEntity<MarketingCampaignEntity, MarketingCampaign>
{
    [Required]
    [StringLength(64)]
    public string Code { get; set; }

    [Required]
    [StringLength(1024)]
    public string Name { get; set; }

    [StringLength(4096)]
    public string Description { get; set; }

    [StringLength(128)]
    public string StoreId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ObservableCollection<MarketingCampaignPromotionEntity> Promotions { get; set; }
        = new NullCollection<MarketingCampaignPromotionEntity>();

    public MarketingCampaign ToModel(MarketingCampaign model)
    {
        model.Id = Id;
        model.CreatedDate = CreatedDate;
        model.ModifiedDate = ModifiedDate;
        model.CreatedBy = CreatedBy;
        model.ModifiedBy = ModifiedBy;

        model.Code = Code;
        model.Name = Name;
        model.Description = Description;
        model.StoreId = StoreId;
        model.IsActive = IsActive;
        model.StartDate = StartDate;
        model.EndDate = EndDate;
        model.PromotionIds = Promotions.Select(x => x.PromotionId).ToList();

        return model;
    }

    public MarketingCampaignEntity FromModel(MarketingCampaign model, PrimaryKeyResolvingMap pkMap)
    {
        pkMap.AddPair(model, this);

        Id = model.Id;
        CreatedDate = model.CreatedDate;
        ModifiedDate = model.ModifiedDate;
        CreatedBy = model.CreatedBy;
        ModifiedBy = model.ModifiedBy;

        Code = model.Code;
        Name = model.Name;
        Description = model.Description;
        StoreId = model.StoreId;
        IsActive = model.IsActive;
        StartDate = model.StartDate;
        EndDate = model.EndDate;

        if (model.PromotionIds != null)
        {
            Promotions = new ObservableCollection<MarketingCampaignPromotionEntity>(
                model.PromotionIds.Select(promotionId => new MarketingCampaignPromotionEntity
                {
                    CampaignId = model.Id,
                    PromotionId = promotionId,
                }));
        }

        return this;
    }

    public void Patch(MarketingCampaignEntity target)
    {
        target.Code = Code;
        target.Name = Name;
        target.Description = Description;
        target.StoreId = StoreId;
        target.IsActive = IsActive;
        target.StartDate = StartDate;
        target.EndDate = EndDate;

        if (!Promotions.IsNullCollection())
        {
            Promotions.Patch(target.Promotions, (source, dest) => { });
        }
    }
}
