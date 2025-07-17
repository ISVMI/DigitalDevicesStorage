FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]

COPY ["Services/Auth/DigitalDevices.AuthService.Application/DigitalDevices.AuthService.Application.csproj", "Services/Auth/DigitalDevices.AuthService.Application/"]
COPY ["Services/Auth/DigitalDevices.AuthService.Infrastructure/DigitalDevices.AuthService.Infrastructure.csproj", "Services/Auth/DigitalDevices.AuthService.Infrastructure/"]
COPY ["Services/Auth/DigitalDevices.AuthService.Core/DigitalDevices.AuthService.Core.csproj", "Services/Auth/DigitalDevices.AuthService.Core/"]
COPY ["Services/Auth/DigitalDevices.AuthService.Api/DigitalDevices.AuthService.Api.csproj", "Services/Auth/DigitalDevices.AuthService.Api/"]

RUN dotnet restore "Services/Auth/DigitalDevices.AuthService.Api/DigitalDevices.AuthService.Api.csproj"

COPY . ./

RUN dotnet publish "Services/Auth/DigitalDevices.AuthService.Api/DigitalDevices.AuthService.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DigitalDevices.AuthService.Api.dll"]