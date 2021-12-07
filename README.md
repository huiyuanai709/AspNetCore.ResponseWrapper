# AspNetCore.ResponseWrapper
AspNetCore.ResponseWrapper is a HTTP API response wrapper, It supports various Action return type, Model invalid wrapper, Swagger response display and also supports custom response wrapper.

## Features:

1. ModelInvalid response wrapper
2. ObjectResult/EmptyResult response wrapper
3. Swagger response wrapped type display
4. Custom response wrapper
5. Disable response wrapper for specified Controller/Action

## Usage

1. Basic
```c#
builder.Services.AddControllers().AddResponseWrapper();
```
2. Disable response wrapper
```C#
[DisableWrapper]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
}
```

or 
```C#
[DisableWrapper]
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
```

See samples...

## Installation
```shell
dotnet add Package AspNetCore.ResponseWrapper
```