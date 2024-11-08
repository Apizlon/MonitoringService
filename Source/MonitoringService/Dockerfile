FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

FROM sdk AS restore
WORKDIR /app
COPY *.sln .

RUN mkdir -p MonitoringService.Application MonitoringService.Contracts MonitoringService.Host MonitoringService.Tests
COPY MonitoringService.Application/MonitoringService.Application.csproj MonitoringService.Application/
COPY MonitoringService.Contracts/MonitoringService.Contracts.csproj MonitoringService.Contracts/
COPY MonitoringService.Host/MonitoringService.Host.csproj MonitoringService.Host/
COPY MonitoringService.Tests/MonitoringService.Tests.csproj MonitoringService.Tests/
RUN dotnet restore

FROM restore AS test
COPY . .
RUN dotnet build --no-restore --configuration Release
CMD dotnet test MonitoringService.Tests/bin/Release/net8.0/MonitoringService.Tests.dll --logger "console;verbosity=detailed"

FROM test AS docs
WORKDIR /app
ENV ConnectionStrings__StatisticsDatabaseConnection=""
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSl https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

RUN npm install nswag -g
RUN nswag aspnetcore2openapi /configuration:Release /nobuild:true /project:MonitoringService.Host/MonitoringService.Host.csproj /output:artifacts/swagger-output.json

FROM restore AS dev-build
COPY . .
RUN dotnet publish --no-restore --configuration Release -o /app/artifacts
COPY :/app/artifacts /artifacts
ENTRYPOINT [ "dotnet", "MonitoringService.Host.dll" ]