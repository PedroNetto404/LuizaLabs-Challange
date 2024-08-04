using FavoriteProducts.Infrastructure.Data.Relational;
using FavoriteProducts.Presentation.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Presentation.Extensions;

public static class Pipeline
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var databaseSeeder = services.GetRequiredService<DatabaseSeeder>();
        await databaseSeeder.SeedAsync();
    }

    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var database = 
            services
                .GetRequiredService<FavoriteProductsContext>()
                .Database;

        await database.EnsureCreatedAsync();
        await database.MigrateAsync();
    }
}