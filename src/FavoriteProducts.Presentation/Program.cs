using FavoriteProducts.Domain.Extensions;
using FavoriteProducts.Infrastructure.Extensions;
using FavoriteProducts.Presentation.Extensions;
using FavoriteProducts.UseCases.Extensions;
using Serilog;

SetupLogger();

var builder = WebApplication.CreateBuilder(args);
builder
    .Services
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentationServices()
    .AddDomainServices()
    .AddUseCasesServices();

var app = builder.Build();

app.UsePipeline();

await app.ApplyMigrationAsync();
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}

try
{
    Log.Information("Starting web host");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

return;

static void SetupLogger() => Log.Logger =
    new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Minute)
        .WriteTo.Seq("http://seq:5341")
        .Enrich.FromLogContext()
        .CreateLogger();