using System;

namespace Weather_Api_Proj.Models.DTOs.Responses;

public class WeatherResponse
{
    public string Description { get; set; } = string.Empty;
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public double WindSpeed { get; set; }
    public string City { get; set; } = string.Empty;
}
