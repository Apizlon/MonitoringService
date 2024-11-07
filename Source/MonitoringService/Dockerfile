FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

FROM sdk AS restore
WORKDIR /app
COPY *.sln .

RUN mkdir -p MonitoringService.Application MonitoringService.Contracts MonitoringService.Host MonitoringService.Tests
COPY MonitoringService.Application/MonitoringService.Application.csproj MonitoringService.Application/MonitoringService.Application.csproj
COPY MonitoringService.Contracts/MonitoringService.Contracts.csproj MonitoringService.Contracts/MonitoringService.Contracts.csproj
COPY MonitoringService.Host/MonitoringService.Host.csproj MonitoringService.Host/MonitoringService.Host.csproj
COPY MonitoringService.Tests/MonitoringService.Tests.csproj MonitoringService.Tests/MonitoringService.Tests.csproj
RUN dotnet restore 

FROM restore AS test
COPY . .
RUN dotnet test --no-restore

FROM test AS docs

FROM restore AS dev-build
COPY . .
RUN dotnet build --no-restore --configuration Release