using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteProducts.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection container, IConfiguration configuration)
    {
        container.AddDbContext<FavoriteProductsContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(FavoriteProductsContext).Assembly.FullName)
            );

            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        return container;
    }
}
