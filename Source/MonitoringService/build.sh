#!/bin/bash

# Прекращаем выполнение при ошибке
set -e

# Проверка, передан ли аргумент
if [ -z "$1" ]; then
    echo "Необходимо передать аргумент: test, docs или dev-build"
    exit 1
fi

# Установка переменной TARGET
TARGET=$1

# Выполняем сборку на основе переданного аргумента
case $TARGET in
    test)
        echo "Сборка тестового образа..."
        docker buildx bake test
        ;;
    docs)
        echo "Сборка образа для генерации документации..."
        docker buildx bake docs
        CONTAINER_ID=$(docker create monitoring-service-docs)
        docker cp "$CONTAINER_ID":/app/artifacts ./artifacts
        docker rm "$CONTAINER_ID"
        echo "Артефакты извлечены в ./artifacts"
        ;;
    dev-build)
        echo "Сборка образа для разработки..."
        docker buildx bake dev-build
        CONTAINER_ID=$(docker create monitoring-service-dev-build)
        docker cp "$CONTAINER_ID":/app/artifacts ./artifacts
        docker rm "$CONTAINER_ID"
        echo "Артефакты извлечены в ./artifacts"
        ;;
    *)
        echo "Неизвестный тип сборки: $TARGET"
        echo "Используйте: test, docs или dev-build"
        exit 1
        ;;
esac

echo "Сборка завершена."
