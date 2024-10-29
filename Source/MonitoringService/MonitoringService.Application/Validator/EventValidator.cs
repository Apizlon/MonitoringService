using MonitoringService.Application.Exceptions;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Validator;

public static class EventValidator
{
    public static EventRequest Validate(this EventRequest eventRequest)
    {
        if (string.IsNullOrEmpty(eventRequest.Name))
        {
            throw new BadRequestException("Поле Наименования события не может быть пустым");
        }

        if (eventRequest.Name.Length > 50)
        {
            throw new BadRequestException("Наименование события должно быть менее 50 символов");
        }

        if (string.IsNullOrEmpty(eventRequest.Description))
        {
            throw new BadRequestException("Поле Описание не может быть пустым");
        }

        if (eventRequest.Description.Length > 1024)
        {
            throw new BadRequestException("Длина описания не должна превышать 1024 символа");
        }

        if (eventRequest.EventDateTime > DateTime.Now)
        {
            throw new BadRequestException("Дата события не должна превышать текущую");
        }
        return eventRequest;
    }
}