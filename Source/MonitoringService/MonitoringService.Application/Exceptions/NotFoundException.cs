namespace MonitoringService.Application.Exceptions;

/// <summary>
/// Ошибка: не найдена
/// </summary>
public abstract class NotFoundException : Exception
{
    /// <summary>
    /// Вызывает конструктор базового класса
    /// </summary>
    /// <param name="message">Текст ошибки</param>
    protected NotFoundException(string message) : base(message)
    {
    }
}