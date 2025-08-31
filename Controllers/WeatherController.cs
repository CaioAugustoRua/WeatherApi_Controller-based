using Microsoft.AspNetCore.Mvc;
using Weather_Api_Proj.Services;

namespace Weather_Api_Proj.Controllers;

/// <summary>
/// controller to handle weather-related requests.
/// </summary>
[ApiController, Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>
    /// Return the weather data for a given city.
    /// </summary>
    /// <param name="city">City name</param>
    /// <returns>Weather data</returns>
    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        try
        {
            var weatherData = await _weatherService.GetWeatherAsync(city);
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
