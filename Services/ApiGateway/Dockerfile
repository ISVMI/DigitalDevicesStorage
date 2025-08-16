FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Common/Shared/Shared.csproj", "Common/Shared/"]

COPY ["Services/ApiGateway/DigitalDevices.ApiGateway.Api/DigitalDevices.ApiGateway.Api.csproj", "Services/ApiGateway/DigitalDevices.ApiGateway.Api/"]

RUN dotnet restore "Services/ApiGateway/DigitalDevices.ApiGateway.Api/DigitalDevices.ApiGateway.Api.csproj"

COPY . ./

RUN dotnet publish "Services/ApiGateway/DigitalDevices.ApiGateway.Api/DigitalDevices.ApiGateway.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "DigitalDevices.ApiGateway.Api.dll"]