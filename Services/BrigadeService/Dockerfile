FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ./Services/BrigadeService/BrigadeService.csproj ./Services/BrigadeService/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore "./Services/BrigadeService/BrigadeService.csproj"

COPY . .

RUN dotnet build "./Services/BrigadeService/BrigadeService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "BrigadeService.dll"]