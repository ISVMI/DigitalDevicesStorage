FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]
COPY ["Services/Manufacturers/DigitalDevices.ManufacturersService.Application/DigitalDevices.ManufacturersService.Application.csproj", "Services/Manufacturers/DigitalDevices.ManufacturersService.Application/"]
COPY ["Services/Manufacturers/DigitalDevices.ManufacturersService.Infrastructure/DigitalDevices.ManufacturersService.Infrastructure.csproj", "Services/Manufacturers/DigitalDevices.ManufacturersService.Infrastructure/"]
COPY ["Services/Manufacturers/DigitalDevices.ManufacturersService.Core/DigitalDevices.ManufacturersService.Core.csproj", "Services/Manufacturers/DigitalDevices.ManufacturersService.Core/"]
COPY ["Services/Manufacturers/DigitalDevices.ManufacturersService.Api/DigitalDevices.ManufacturersService.Api.csproj", "Services/Manufacturers/DigitalDevices.ManufacturersService.Api/"]

RUN dotnet restore "Services/Manufacturers/DigitalDevices.ManufacturersService.Api/DigitalDevices.ManufacturersService.Api.csproj"

COPY . .

RUN dotnet publish "Services/Manufacturers/DigitalDevices.ManufacturersService.Api/DigitalDevices.ManufacturersService.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DigitalDevices.ManufacturersService.Api.dll"]