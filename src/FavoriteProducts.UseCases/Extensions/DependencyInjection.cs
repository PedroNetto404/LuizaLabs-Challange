using FavoriteProducts.UseCases.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FavoriteProducts.UseCases.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection container)
    {
        container.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(CachedQueryBehavior<,>));
        });

        container.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return container;
    }
}
