#!/bin/bash

# Переменные для порта и URL Swagger документации
PORT=5009
SWAGGER_URL="http://localhost:$PORT/swagger/v1/swagger.json"
OUTPUT_DIR="docs"
OUTPUT_FILE="$OUTPUT_DIR/redoc.html"

# Проверка, установлен ли .NET SDK
if ! command -v dotnet &> /dev/null; then
echo ".NET SDK не установлен. Пожалуйста, установите .NET SDK, чтобы продолжить."
exit 1
fi

# Запуск ASP.NET Core приложения в фоне
echo "Запуск приложения ASP.NET Core..."
dotnet run --urls="http://localhost:$PORT" &
APP_PID=$!

# Ожидание, пока приложение полностью не запустится
echo "Ожидание запуска приложения..."
sleep 5

# Проверка, доступен ли Swagger JSON
if curl --output /dev/null --silent --head --fail "$SWAGGER_URL"; then
echo "Swagger JSON доступен по адресу $SWAGGER_URL"
else
echo "Ошибка: Swagger JSON не доступен. Проверьте настройки приложения."
kill $APP_PID
exit 1
fi

# Создание директории для документации
mkdir -p "$OUTPUT_DIR"

# Генерация HTML документации с помощью redoc-cli
echo "Генерация документации с помощью redoc-cli..."
redoc-cli bundle "$SWAGGER_URL" -o "$OUTPUT_FILE"

echo "Документация успешно сгенерирована: $OUTPUT_FILE"

Остановка приложения ASP.NET Core
kill $APP_PID