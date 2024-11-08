#!/bin/bash

./build.sh docs

docker run \
    --name "monitoring-service-docs" \
    --rm \
    "monitoring-service-docs"