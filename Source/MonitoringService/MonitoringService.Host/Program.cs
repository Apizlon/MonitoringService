using FluentMigrator.Runner;
using MonitoringService.Application;
using MonitoringService.Application.Extensions;
using MonitoringService.Application.Migrations;
using MonitoringService.Host.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Конфигурация логгера в файле appsetings.Development.json.
//Seq запускается с помощью docker-compose.yml 
builder.Host.UseSerilog((context,services,configuration)=>
    configuration.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<StatisticsDbContext>();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddProcessors();
builder.Services.AddTransient<CustomExceptionHandlingMiddleware>();
builder.Services.AddMigration(builder.Configuration);

var app = builder.Build();
app.Logger.LogInformation("Сборка успешно завершена");

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigin");
app.UseMiddleware<CustomExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Logger.LogInformation("Запуск приложения");
app.Run();
