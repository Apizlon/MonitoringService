namespace MonitoringService.Application.Exceptions;

/// <summary>
/// Ошибка не найденной статистики
/// </summary>
public class StatisticsNotFoundException : NotFoundException
{
    /// <summary>
    /// Вызывает конструктор базового класса
    /// </summary>
    /// <param name="id">введенное пользователем id</param>
    public StatisticsNotFoundException(ulong id) : base($"Данные об устройстве с id {id} не найдены.")
    {
        
    }
}