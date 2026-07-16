FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8080

ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Production


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["src/OnibusExpress.Api/OnibusExpress.Api.csproj", "src/OnibusExpress.Api/"]
COPY ["src/OnibusExpress.Application/OnibusExpress.Application.csproj", "src/OnibusExpress.Application/"]
COPY ["src/OnibusExpress.Domain/OnibusExpress.Domain.csproj", "src/OnibusExpress.Domain/"]
COPY ["src/OnibusExpress.Infrastructure/OnibusExpress.Infrastructure.csproj", "src/OnibusExpress.Infrastructure/"]

RUN dotnet restore "src/OnibusExpress.Api/OnibusExpress.Api.csproj"

COPY . .

WORKDIR "/src/src/OnibusExpress.Api"

RUN dotnet build "OnibusExpress.Api.csproj" \
    --configuration Release \
    --output /app/build \
    --no-restore


FROM build AS publish

RUN dotnet publish "OnibusExpress.Api.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false


# Imagem final
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OnibusExpress.Api.dll"]