FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]

COPY ["Services/Characteristics/DigitalDevices.CharacteristicsService.Application/DigitalDevices.CharacteristicsService.Application.csproj", "Services/Characteristics/DigitalDevices.CharacteristicsService.Application/"]
COPY ["Services/Characteristics/DigitalDevices.CharacteristicsService.Infrastructure/DigitalDevices.CharacteristicsService.Infrastructure.csproj", "Services/Characteristics/DigitalDevices.CharacteristicsService.Infrastructure/"]
COPY ["Services/Characteristics/DigitalDevices.CharacteristicsService.Core/DigitalDevices.CharacteristicsService.Core.csproj", "Services/Characteristics/DigitalDevices.CharacteristicsService.Core/"]
COPY ["Services/Characteristics/DigitalDevices.CharacteristicsService.Api/DigitalDevices.CharacteristicsService.Api.csproj", "Services/Characteristics/DigitalDevices.CharacteristicsService.Api/"]

RUN dotnet restore "Services/Characteristics/DigitalDevices.CharacteristicsService.Api/DigitalDevices.CharacteristicsService.Api.csproj"

COPY . ./

RUN dotnet publish "Services/Characteristics/DigitalDevices.CharacteristicsService.Api/DigitalDevices.CharacteristicsService.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DigitalDevices.CharacteristicsService.Api.dll"]