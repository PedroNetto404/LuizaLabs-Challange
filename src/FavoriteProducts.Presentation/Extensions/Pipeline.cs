﻿using FavoriteProducts.Infrastructure.Data.Relational;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Presentation.Extensions;

public static class Pipeline
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        app.UseExceptionHandler();

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

    public static WebApplication SeedDatabaseIfDevelopment(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        services
            .GetRequiredService<DatabaseSeed>()
            .SeedAsync()
            .Wait();

        return app;
    }

    public static WebApplication ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services
            .GetRequiredService<FavoriteProductsContext>();

        if (!context.Database.CanConnect()) return app;
        
        context.Database.Migrate();

        return app;
    }
}