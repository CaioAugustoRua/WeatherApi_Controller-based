using Microsoft.AspNetCore.Mvc;
using Weather_Api_Proj.Models.Enumerations;
using Weather_Api_Proj.Services.Interfaces;

namespace Weather_Api_Proj.Controllers;

/// <summary>
/// controller to handle weather-related requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>
    /// Get weather information for a specified city.
    /// </summary>
    /// <param name="city"></param>
    /// <param name="unitOfMeasure"></param>
    /// <param name="cultureCode"></param>
    /// <returns></returns>
    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city, [FromQuery] UnitOfMeasureEnum unitOfMeasure, [FromQuery] string cultureCode)
    {
        try
        {
            var weatherData = await _weatherService.GetWeatherAsync(city, unitOfMeasure, cultureCode);
            return Ok(weatherData);
        }
        catch (HttpRequestException httpEx)
        {
            return StatusCode(503, $"Error accessing weather service: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }
}