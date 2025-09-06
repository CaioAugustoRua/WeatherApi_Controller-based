using Weather_Api_Proj.Services;
using Weather_Api_Proj.Configurations;
using Weather_Api_Proj.Services.Interfaces;

Console.WriteLine("=== Starting Weather API Application ===");

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("WebApplicationBuilder created.");

builder.Services.AddHttpClient<IWeatherService, WeatherService>();
Console.WriteLine("HttpClient registered for WeatherService.");

builder.Services.AddControllers();
Console.WriteLine("Controllers added to DI container.");

Console.WriteLine("Configuring WeatherApiSettings...");
builder.Services.Configure<OpenWeatherMapApiSettings>(
    builder.Configuration.GetSection("OpenWeatherMap"));
Console.WriteLine("WeatherApiSettings configured.");

Console.WriteLine("Configuring Swagger...");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine("Swagger configured.");

var app = builder.Build();
Console.WriteLine("WebApplication built.");

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development environment detected: enabling Swagger UI.");
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.WriteLine("Enabling HTTPS redirection...");
app.UseHttpsRedirection();

Console.WriteLine("Enabling Authorization middleware...");
app.UseAuthorization();

Console.WriteLine("Mapping controllers...");
app.MapControllers();

Console.WriteLine("Running application...");
app.Run();