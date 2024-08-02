using System.Text.Json;
using System.Text.Json.Serialization;
using FavoriteProducts.Presentation.Handlers;
using Microsoft.OpenApi.Models;

namespace FavoriteProducts.Presentation.Extensions;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection container)
    {
        container
            .AddControllers()
            .AddJsonOptions(p =>
        {
            p.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
            p.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
        
        container.AddSwagger();
        container.AddExceptionHandler<GlobalExceptionHandler>();
        container.AddProblemDetails();

        return container;
    }

    private static void AddSwagger(this IServiceCollection container)
    {
        container.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Desafio Técnico LuizaLabs - Gerenciamento de Produtos Favoritos",
                    Description = "RESTFull API para gerenciamento de produtos favoritos",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "pedronetto31415@gmail.com",
                        Name = "Pedro Netto",
                        Url = new Uri("https://github.com/PedroNetto404")
                    }
                }
            );
        });
    }
}
