FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ./Services/FileService/FileService.csproj ./Services/FileService/
COPY ./Shared/Shared.csproj ./Shared/

RUN dotnet restore "./Services/FileService/FileService.csproj"

COPY . .

RUN dotnet build "./Services/FileService/FileService.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "FileService.dll"]
