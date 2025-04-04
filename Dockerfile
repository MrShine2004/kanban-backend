# Базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только файлы проектов (ускоряет восстановление зависимостей)
COPY ["KanbanApp.API/KanbanApp.API.csproj", "KanbanApp.API/"]
COPY ["KanbanApp.Core/KanbanApp.Core.csproj", "KanbanApp.Core/"]
COPY ["KanbanApp.DataAccess/KanbanApp.DataAccess.csproj", "KanbanApp.DataAccess/"]
COPY ["KannbanApp.Application/KanbanApp.Application.csproj", "KannbanApp.Application/"]

# Восстанавливаем зависимости
RUN dotnet restore "KanbanApp.API/KanbanApp.API.csproj"

# Копируем весь исходный код
COPY . .

# Собираем и публикуем
RUN dotnet publish "KanbanApp.API/KanbanApp.API.csproj" -c Release -o /app

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Настройки среды
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "KanbanApp.API.dll"]