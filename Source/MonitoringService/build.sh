#!/bin/bash

#Прекращаем выполнение при ошибке
set -e

#Проверка, передан ли аргумент
if [ -z "$1" ]; then
echo "Необходимо передать аргумент: test, docs или dev-build"
exit 1
fi

#Установка переменных
TARGET=$1
IMAGE_NAME="MonitoringService:$TARGET"

#Выполняем сборку на основе переданного аргумента
case $TARGET in
    test)
        echo "Сборка тестового образа..."
        docker build --target test -t "$IMAGE_NAME" .
        ;;
    docs)
        echo "Сборка образа для генерации документации..."
        docker build --target docs -t "$IMAGE_NAME" .
        ;;
    dev-build)
        echo "Сборка образа для разработки..."
        docker build --target dev-build -t "$IMAGE_NAME" .

        docker-compose up --build
        ;;
    *)
        echo "Неизвестный тип сборки: $TARGET"
        echo "Используйте: test, docs или dev-build"
        exit 1
        ;;
esac

echo "Сборка завершена. Образ создан: $IMAGE_NAME"