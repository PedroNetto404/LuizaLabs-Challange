FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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

# Install vsdbg for debugging
RUN apt-get update && \
    apt-get install -y unzip && \
    apt-get install -y procps && \
    mkdir -p /root/vsdbg && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /root/vsdbg

# Install openssl
RUN apt-get update && apt-get install -y openssl

# Create the PFX certificate
RUN mkdir /https && \
    openssl req -x509 -newkey rsa:4096 -keyout /https/aspnetapp.key -out /https/aspnetapp.crt -days 365 -nodes -subj "/CN=localhost" && \
    openssl pkcs12 -export -out /https/aspnetapp.pfx -inkey /https/aspnetapp.key -in /https/aspnetapp.crt -passout pass:1312_3213@!4123

ENTRYPOINT ["dotnet", "FavoriteProducts.Presentation.dll"]
