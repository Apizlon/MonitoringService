#!/bin/bash

./build.sh test

docker run \
    --name "monitoring-service-test" \
    --rm \
    "monitoring-service-test"