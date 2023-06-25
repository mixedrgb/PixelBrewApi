FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5010

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PixelBrewApi.csproj", "./"]
RUN dotnet restore "PixelBrewApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PixelBrewApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixelBrewApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixelBrewApi.dll"]
