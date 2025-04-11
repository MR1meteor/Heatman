FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

RUN apt-get update && apt-get install -y ca-certificates

COPY Shared.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet build -c Release -o /app/build

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .