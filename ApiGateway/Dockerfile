FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build "ApiGateway.csproj"
WORKDIR /src
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ApiGateway.dll"]