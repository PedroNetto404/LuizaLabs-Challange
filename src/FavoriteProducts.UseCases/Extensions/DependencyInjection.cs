using Microsoft.Extensions.DependencyInjection;

namespace FavoriteProducts.UseCases.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection container)
    {
        container.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        return container;
    }
}
