using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.MarketingCampaigns.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.MarketingCampaigns.Data.Repositories;

public class MarketingCampaignsDbContext : DbContextBase
{
    public MarketingCampaignsDbContext(DbContextOptions<MarketingCampaignsDbContext> options)
        : base(options)
    {
    }

    protected MarketingCampaignsDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MarketingCampaignEntity>().ToTable("MarketingCampaign").HasKey(x => x.Id);
        modelBuilder.Entity<MarketingCampaignEntity>().Property(x => x.Id).HasMaxLength(IdLength).ValueGeneratedOnAdd();

        modelBuilder.Entity<MarketingCampaignPromotionEntity>().ToTable("MarketingCampaignPromotion").HasKey(x => x.Id);
        modelBuilder.Entity<MarketingCampaignPromotionEntity>().Property(x => x.Id).HasMaxLength(IdLength).ValueGeneratedOnAdd();
        modelBuilder.Entity<MarketingCampaignPromotionEntity>()
            .HasOne(x => x.Campaign)
            .WithMany(x => x.Promotions)
            .HasForeignKey(x => x.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.MarketingCampaigns.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.MarketingCampaigns.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.MarketingCampaigns.Data.SqlServer"));
                break;
        }
    }
}
