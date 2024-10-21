using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Validator;

public static class StatisticsValidator
{
    public static StatisticsRequest Validate(this StatisticsRequest statistics)
    {
        if (string.IsNullOrEmpty(statistics.DeviceName))
        {
            throw new BadRequestException("Поле Имя устройства не может быть пустым!");
        }
        if (string.IsNullOrEmpty(statistics.OperatingSystem))
        {
            throw new BadRequestException("Поле Операционная система не может быть пустым!");
        }
        if(string.IsNullOrEmpty(statistics.Version))
        {
            throw new BadRequestException("Поле версия не может быть пустым!");
        }

        return statistics;
    }
}