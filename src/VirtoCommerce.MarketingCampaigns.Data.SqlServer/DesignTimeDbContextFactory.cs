using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.MarketingCampaigns.Data.Repositories;

namespace VirtoCommerce.MarketingCampaigns.Data.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MarketingCampaignsDbContext>
{
    public MarketingCampaignsDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MarketingCampaignsDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=(local);User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(typeof(SqlServerDataAssemblyMarker).Assembly.GetName().Name));

        return new MarketingCampaignsDbContext(builder.Options);
    }
}
