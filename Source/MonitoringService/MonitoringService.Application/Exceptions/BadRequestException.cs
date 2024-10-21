namespace MonitoringService.Application.Exceptions;

/// <summary>
/// Ошибка пользовательского ввода
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Вызывает конструктор базового класса
    /// </summary>
    /// <param name="message">Текст ошибки</param>
    public BadRequestException(string message) : base(message)
    {
    }
}