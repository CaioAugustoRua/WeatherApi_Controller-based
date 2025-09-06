using Weather_Api_Proj.Models.DTOs.Responses;
using Weather_Api_Proj.Models.Enumerations;

namespace Weather_Api_Proj.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponse?> GetWeatherAsync(string city, UnitOfMeasureEnum unitOfMeasure, string cultureCode = "en");
}
