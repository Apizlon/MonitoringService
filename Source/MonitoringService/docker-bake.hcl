group "default" {
  targets = ["dev-build"]
}

target "test" {
  dockerfile = "./Dockerfile"
  target     = "test"
  context    = "."
  tags       = ["monitoring-service-test"]
}

target "docs" {
  dockerfile = "./Dockerfile"
  target     = "docs"
  context    = "."
  tags       = ["monitoring-service-docs"]
}

target "dev-build" {
  dockerfile = "./Dockerfile"
  target     = "dev-build"
  context    = "."
  tags       = ["monitoring-service-dev-build"]
}
