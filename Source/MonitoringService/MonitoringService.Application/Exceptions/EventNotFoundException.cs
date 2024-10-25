namespace MonitoringService.Application.Exceptions;

public class EventNotFoundException : NotFoundException
{
    public EventNotFoundException(Guid id) : base($"Данные о событии с id {id} не найдены.")
    {
        
    }
}