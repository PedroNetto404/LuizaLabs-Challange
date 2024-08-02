FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FavoriteProducts.Presentation/FavoriteProducts.Presentation.csproj", "src/FavoriteProducts.Presentation/"]
COPY ["src/FavoriteProducts.UseCases/FavoriteProducts.UseCases.csproj", "src/FavoriteProducts.UseCases/"]
COPY ["src/FavoriteProducts.Domain/FavoriteProducts.Domain.csproj", "src/FavoriteProducts.Domain/"]
COPY ["src/FavoriteProducts.Infrastructure/FavoriteProducts.Infrastructure.csproj", "src/FavoriteProducts.Infrastructure/"]
RUN dotnet restore "./src/FavoriteProducts.Presentation/FavoriteProducts.Presentation.csproj"
COPY . .
WORKDIR "/src/src/FavoriteProducts.Presentation"
RUN dotnet build "FavoriteProducts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FavoriteProducts.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoriteProducts.Presentation.dll"]