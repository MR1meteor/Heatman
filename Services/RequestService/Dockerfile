FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ./Services/RequestService/RequestService.csproj ./Services/RequestService/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore "./Services/RequestService/RequestService.csproj"

COPY . .

RUN dotnet build "./Services/RequestService/RequestService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "RequestService.dll"]
