# Seq
запускается через docker-compose up -d

# nswag how to use
dotnet tool install --global NSwag.ConsoleCore
Вы можете вызвать это средство с помощью следующей команды: nswag
Средство "nswag.consolecore" (версии "14.1.0") успешно установлено.

nswag openapi2csclient /input:https://localhost:7067/swagger/v1/swagger.json /output:ApiClient.cs

#versions
angular 15 node 18.20.4 npm 10.7.0
