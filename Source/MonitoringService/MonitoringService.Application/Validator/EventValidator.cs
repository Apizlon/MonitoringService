using MonitoringService.Application.Exceptions;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Validator;

/// <summary>
/// Класс для валидации входящих запросов типа <see cref="EventRequest"/>>
/// </summary>
public static class EventValidator
{
    /// <summary>
    /// Метод расширения для валидации
    /// </summary>
    /// <param name="eventRequest">Объект <see cref="EventRequest"/>> входящего запроса</param>
    /// <returns><see cref="EventRequest"/></returns>
    /// <exception cref="BadRequestException"><see cref="BadRequestException"/>></exception>
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