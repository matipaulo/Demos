#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket.Api/Basket.Api.csproj", "Services/Basket.Api/"]
RUN dotnet restore "Services/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Services/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build -r win-x64 --self-contained false --no-restore

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]