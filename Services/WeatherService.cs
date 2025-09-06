using System.Text.Json;
using Microsoft.Extensions.Options;
using Weather_Api_Proj.Configurations;
using Weather_Api_Proj.Models.DTOs.Responses;
using Weather_Api_Proj.Models.Enumerations;
using Weather_Api_Proj.Services.Interfaces;

namespace Weather_Api_Proj.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherMapApiSettings _settings;

    public WeatherService(HttpClient httpClient, IOptions<OpenWeatherMapApiSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(string city, UnitOfMeasureEnum unitOfMeasure = UnitOfMeasureEnum.Standard, string cultureCode = "en")
    {
        var url = $"{_settings.BaseUrl}weather?q={city}&appid={_settings.ApiKey}&units={unitOfMeasure}&lang={cultureCode}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var weatherResponse = new WeatherResponse
        {
            Description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? string.Empty,
            Temp = root.GetProperty("main").GetProperty("temp").GetDouble(),
            FeelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
            TempMin = root.GetProperty("main").GetProperty("temp_min").GetDouble(),
            TempMax = root.GetProperty("main").GetProperty("temp_max").GetDouble(),
            WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
            City = root.GetProperty("name").GetString() ?? string.Empty
        };

        return weatherResponse;
    }
}
