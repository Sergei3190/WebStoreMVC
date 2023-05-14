#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebStoreMVC/WebStoreMVC.csproj", "WebStoreMVC/"]
COPY ["WebStore.DAL/WebStoreMVC.DAL.csproj", "WebStore.DAL/"]
COPY ["WebStoreMVC.Domain/WebStoreMVC.Domain.csproj", "WebStoreMVC.Domain/"]
COPY ["WebStoreMVC.Sqlite/WebStoreMVC.DAL.Sqlite.csproj", "WebStoreMVC.Sqlite/"]
RUN dotnet restore "WebStoreMVC/WebStoreMVC.csproj"
COPY . .
WORKDIR "/src/WebStoreMVC"
RUN dotnet build "WebStoreMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebStoreMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStoreMVC.dll"]