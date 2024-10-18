namespace MonitoringService.Application.Exceptions;

public class StatisticsNotFoundException : NotFoundException
{
    public StatisticsNotFoundException(ulong id) : base($"Данные об устройстве с id {id} не найдены.")
    {
        
    }
}