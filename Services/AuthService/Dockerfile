FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

RUN apt-get update && apt-get install -y ca-certificates

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ./Services/AuthService/AuthService.csproj ./Services/AuthService/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore "./Services/AuthService/AuthService.csproj"

COPY . .

RUN dotnet build "./Services/AuthService/AuthService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "AuthService.dll"]