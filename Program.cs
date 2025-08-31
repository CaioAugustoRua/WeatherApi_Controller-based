using Weather_Api_Proj.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpClient
builder.Services.AddHttpClient<WeatherService>();

// Add controllers support
builder.Services.AddControllers();

// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
