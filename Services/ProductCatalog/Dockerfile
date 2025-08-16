FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]

COPY ["Services/ProductCatalog/DigitalDevices.ProductCatalogService.Application/DigitalDevices.ProductCatalogService.Application.csproj", "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Application/"]
COPY ["Services/ProductCatalog/DigitalDevices.ProductCatalogService.Infrastructure/DigitalDevices.ProductCatalogService.Infrastructure.csproj", "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Infrastructure/"]
COPY ["Services/ProductCatalog/DigitalDevices.ProductCatalogService.Core/DigitalDevices.ProductCatalogService.Core.csproj", "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Core/"]
COPY ["Services/ProductCatalog/DigitalDevices.ProductCatalogService.Api/DigitalDevices.ProductCatalogService.Api.csproj", "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Api/"]

RUN dotnet restore "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Api/DigitalDevices.ProductCatalogService.Api.csproj"

COPY . ./

RUN dotnet publish "Services/ProductCatalog/DigitalDevices.ProductCatalogService.Api/DigitalDevices.ProductCatalogService.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DigitalDevices.ProductCatalogService.Api.dll"]