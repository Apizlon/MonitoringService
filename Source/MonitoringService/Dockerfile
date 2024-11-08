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

FROM restore AS dev-build
COPY . .
RUN dotnet publish --no-restore --configuration Release -o /app/artifacts
ENTRYPOINT [ "dotnet", "MonitoringService.Host.dll" ]