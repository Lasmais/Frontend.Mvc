using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly bool isTestData;
    private readonly IConfiguration _configuration;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        //test
        _logger = logger;
        _configuration = configuration;
        isTestData = configuration.GetValue<bool>("Feature:TestData");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        if (isTestData)
        {
            return Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
.ToArray();
        }
        string result = "";
        foreach (var key in _configuration.GetChildren())
        {
            result += " |KeyValue: " + key.Key + " " + key.Value;
        }
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = index == 1 ? result : Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

