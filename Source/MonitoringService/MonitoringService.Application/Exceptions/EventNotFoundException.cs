namespace MonitoringService.Application.Exceptions;

/// <summary>
/// Ошибка не найденного события
/// </summary>
public class EventNotFoundException : NotFoundException
{
    /// <summary>
    /// Вызывает конструктор базового класса
    /// </summary>
    /// <param name="id">введенное пользователем id</param>
    public EventNotFoundException(Guid id) : base($"Данные о событии с id {id} не найдены.")
    {
        
    }
}