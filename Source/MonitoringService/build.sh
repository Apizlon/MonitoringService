#!/bin/bash

set -e

PROJECT_NAME="MonitoringApplication"
SOLUTION_FILE="MonitoringService.sln"
OUTPUT_DIR="artifacts"

if ! command -v dotnet &> /dev/null; then
    echo ".NET SDK не установлен"
    exit 1
fi

echo "Очистка предыдущих сборок..."
dotnet clean "$SOLUTION_FILE"

echo "Восстановление зависимостей..."
dotnet restore "$SOLUTION_FILE"

echo "Сборка проекта..."
dotnet build "$SOLUTION_FILE" --configuration Debug

echo "Запуск тестов..."
dotnet test "$SOLUTION_FILE" --configuration Debug

echo "Публикация сборки..."
dotnet publish "$PROJECT_NAME" --configuration Debug --output "$OUTPUT_DIR"

echo "Сборка завершена."