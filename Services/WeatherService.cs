using System.Text.Json;

namespace Weather_Api_Proj.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new InvalidOperationException("API Key not found in configuration.");
        _baseUrl = configuration["OpenWeatherMap:BaseUrl"] ?? "https://api.openweathermap.org/data/2.5/";
    }

    public async Task<object?> GetWeatherAsync(string city)
    {
        var url = $"{_baseUrl}weather?q={city}&appid={_apiKey}&units=metric&lang=en";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        // DTO type.
        var data = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return data;
    }
}
