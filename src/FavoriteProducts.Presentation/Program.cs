using FavoriteProducts.Infrastructure.Extensions;
using FavoriteProducts.Presentation.Extensions;
using FavoriteProducts.UseCases.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services
        .AddPresentationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddUseCasesServices();
}

await builder
    .Build()
    .UsePipeline()
    .RunAsync();