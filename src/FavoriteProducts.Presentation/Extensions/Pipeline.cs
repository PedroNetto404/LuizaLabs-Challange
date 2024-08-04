using FavoriteProducts.Infrastructure.Data.Relational;
using FavoriteProducts.Presentation.Middlewares;

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

    public static async Task SetupDatabaseAsync(this WebApplication app)
    {
        var services = app.Services;
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<FavoriteProductsContext>();
        var database = context.Database;

        await database.EnsureCreatedAsync();
        
        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }
}