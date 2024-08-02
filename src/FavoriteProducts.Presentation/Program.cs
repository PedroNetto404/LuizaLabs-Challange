using FavoriteProducts.Domain.Extensions;
using FavoriteProducts.Infrastructure.Extensions;
using FavoriteProducts.Presentation.Extensions;
using FavoriteProducts.UseCases.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;

    services
        .AddInfrastructureServices(builder.Configuration)
        .AddPresentationServices()
        .AddDomainServices()
        .AddUseCasesServices();
}

await builder
    .Build()
    .UsePipeline()
    .ApplyMigration()
    .SeedDatabaseIfDevelopment()
    .RunAsync();