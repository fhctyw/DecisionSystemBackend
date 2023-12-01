# Використовуйте базовий образ з .NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

WORKDIR /src
COPY ["DecisionSystem.csproj", "./"]
RUN dotnet restore "DecisionSystem.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet build "DecisionSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DecisionSystem.csproj" -c Release -o /app/publish

# Використовуйте базовий образ з .NET 7 Runtime для запуску
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DecisionSystem.dll"]