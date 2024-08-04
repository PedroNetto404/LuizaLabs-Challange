using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Infrastructure.Cache;
using FavoriteProducts.Infrastructure.Data.Relational;
using FavoriteProducts.UseCases.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FavoriteProducts.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection container, 
        IConfiguration configuration)
    {
        container.AddDbContext<FavoriteProductsContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(FavoriteProductsContext).Assembly.FullName)
            );
            
            options.LogTo(message => Log.ForContext<FavoriteProductsContext>().Information(message));
        });

        container.AddScoped<ICacheProvider, MemoryCacheProvider>();
        container.AddMemoryCache();
        container.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        container.AddTransient<DatabaseSeed>();
        
        return container;
    }
}
