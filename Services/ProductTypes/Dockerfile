FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]

COPY ["Services/ProductTypes/DigitalDevices.ProductTypesService.Application/DigitalDevices.ProductTypesService.Application.csproj", "Services/ProductTypes/DigitalDevices.ProductTypesService.Application/"]
COPY ["Services/ProductTypes/DigitalDevices.ProductTypesService.Infrastructure/DigitalDevices.ProductTypesService.Infrastructure.csproj", "Services/ProductTypes/DigitalDevices.ProductTypesService.Infrastructure/"]
COPY ["Services/ProductTypes/DigitalDevices.ProductTypesService.Core/DigitalDevices.ProductTypesService.Core.csproj", "Services/ProductTypes/DigitalDevices.ProductTypesService.Core/"]
COPY ["Services/ProductTypes/DigitalDevices.ProductTypesService.Api/DigitalDevices.ProductTypesService.Api.csproj", "Services/ProductTypes/DigitalDevices.ProductTypesService.Api/"]

RUN dotnet restore "Services/ProductTypes/DigitalDevices.ProductTypesService.Api/DigitalDevices.ProductTypesService.Api.csproj"

COPY . ./

RUN dotnet publish "Services/ProductTypes/DigitalDevices.ProductTypesService.Api/DigitalDevices.ProductTypesService.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DigitalDevices.ProductTypesService.Api.dll"]