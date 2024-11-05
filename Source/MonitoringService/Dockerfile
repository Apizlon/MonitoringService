FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

FROM sdk AS restore
WORKDIR /app
COPY *.sln .
COPY src/*/*.csproj ./src/
RUN dotnet restore

FROM restore AS test
COPY . .
RUN dotnet test --no-restore

FROM test AS docs

FROM restore AS dev-build
COPY . .
RUN dotnet build --no-restore --configuration Release