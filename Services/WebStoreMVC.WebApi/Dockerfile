#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/WebStoreMVC.WebApi/WebStoreMVC.WebApi.csproj", "Services/WebStoreMVC.WebApi/"]
COPY ["Common/WebStoreMVC.Dto/WebStoreMVC.Dto.csproj", "Common/WebStoreMVC.Dto/"]
COPY ["Common/WebStoreMVC.Domain/WebStoreMVC.Domain.csproj", "Common/WebStoreMVC.Domain/"]
COPY ["Common/WebStoreMVC.ViewModels/WebStoreMVC.ViewModels.csproj", "Common/WebStoreMVC.ViewModels/"]
COPY ["Common/WebStoreMVC.Logging/WebStoreMVC.Logging.csproj", "Common/WebStoreMVC.Logging/"]
COPY ["Data/WebStoreMVC.Sqlite/WebStoreMVC.DAL.Sqlite.csproj", "Data/WebStoreMVC.Sqlite/"]
COPY ["Data/WebStoreMVC.DAL/WebStoreMVC.DAL.csproj", "Data/WebStoreMVC.DAL/"]
COPY ["Services/WebStoreMVC.Services/WebStoreMVC.Services.csproj", "Services/WebStoreMVC.Services/"]
COPY ["Services/WebStoreMVC.Interfaces/WebStoreMVC.Interfaces.csproj", "Services/WebStoreMVC.Interfaces/"]
RUN dotnet restore "Services/WebStoreMVC.WebApi/WebStoreMVC.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/WebStoreMVC.WebApi"
RUN dotnet build "WebStoreMVC.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStoreMVC.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStoreMVC.WebApi.dll"]