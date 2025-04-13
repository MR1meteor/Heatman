FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ./Services/ReportService/ReportService.csproj ./Services/ReportService/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore "./Services/ReportService/ReportService.csproj"

COPY . .

RUN dotnet build "./Services/ReportService/ReportService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "ReportService.dll"]