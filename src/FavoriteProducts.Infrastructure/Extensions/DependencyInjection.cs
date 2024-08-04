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
        _ = container.AddDbContext<FavoriteProductsContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string not found");
            Log.ForContext<FavoriteProductsContext>().Information("Connection string: {ConnectionString}", connectionString);

            options.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly(typeof(FavoriteProductsContext).Assembly.FullName)
            );

            options.LogTo(message => Log.ForContext<FavoriteProductsContext>().Information(message));
        });

        container.AddScoped<ICacheProvider, MemoryCacheProvider>();
        container.AddMemoryCache();
        container.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        container.AddTransient<DatabaseSeeder>();

        return container;
    }
}
