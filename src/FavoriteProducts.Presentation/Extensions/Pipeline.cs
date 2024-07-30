namespace FavoriteProducts.Presentation.Extensions;

public static class Pipeline
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FavoriteProducts API V1");
                c.RoutePrefix = string.Empty;
            });
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
}
