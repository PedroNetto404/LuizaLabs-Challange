using System;
using FavoriteProducts.Domain.Resources.AcrossAggregateServices;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteProducts.Domain.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection container)
    {
        container.AddScoped<FavoriteProductDomainService>();
        return container;
    }
}
