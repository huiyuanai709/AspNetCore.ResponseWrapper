using Microsoft.AspNetCore.Mvc;

namespace DefaultWrapperSample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("GetWeatherForecastAsync")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        await Task.CompletedTask;
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("ActionResultAsync")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetActionResult()
    {
        await Task.CompletedTask;
        return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
    }

    [HttpGet("GetTemperature")]
    public int GetTemperatureC()
    {
        return Random.Shared.Next(-20, 55);
    }
    
    [HttpGet("GetTemperatureAsync")]
    public async Task<int> GetTemperatureCAsync()
    {
        await Task.CompletedTask;
        return Random.Shared.Next(-20, 55);
    }

    [HttpGet("Suitable")]
    public bool Suitable()
    {
        return Random.Shared.Next(-20, 55) >= 20;
    }

    [HttpGet("SuitableAsync")]
    public async Task<bool> SuitableAsync()
    {
        await Task.CompletedTask;
        return Random.Shared.Next(-20, 55) >= 20;
    }

    [HttpGet("Empty")]
    public void Empty()
    {
    }

    [HttpGet("EmptyAsync")]
    public async Task EmptyAsync()
    {
        await Task.CompletedTask;
    }

    [HttpPost]
    public async Task<WeatherForecast> Post(WeatherForecast forecast)
    {
        await Task.CompletedTask;
        return forecast;
    }
}