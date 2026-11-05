# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Grafana-InfluxdbTest/Grafana-InfluxdbTest.csproj", "Grafana-InfluxdbTest/"]
RUN dotnet restore "./Grafana-InfluxdbTest/Grafana-InfluxdbTest.csproj"
COPY . .
WORKDIR "/src/Grafana-InfluxdbTest"
RUN dotnet build "./Grafana-InfluxdbTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Grafana-InfluxdbTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV InfluxDB__Url=http://host.docker.internal:8086
ENTRYPOINT ["dotnet", "Grafana-InfluxdbTest.dll"]