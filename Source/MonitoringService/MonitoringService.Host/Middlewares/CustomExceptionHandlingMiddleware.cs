using MonitoringService.Application.Exceptions;

namespace MonitoringService.Host.Middlewares;

/// <summary>
/// Middleware обработки ошибок
/// </summary>
internal class CustomExceptionHandlingMiddleware : IMiddleware
{
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="logger">Логгер</param>
    public CustomExceptionHandlingMiddleware(ILogger<CustomExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// Отлавливает ошибки во входящих запросах
    /// </summary>
    /// <param name="context" ><see cref="HttpContent"/></param>
    /// <param name="next"><see cref="RequestDelegate"/></param>
    public async Task InvokeAsync(HttpContext context,RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e,e.Message);
            await HandleExceptionAsync(context, e);
        }
    }
    
    /// <summary>
    /// Обрабатывает полученные ошибки и формирует ответ
    /// </summary>
    /// <param name="context" ><see cref="HttpContent"/></param>
    /// <param name="exception"><see cref="Exception"/></param>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            error = exception.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}