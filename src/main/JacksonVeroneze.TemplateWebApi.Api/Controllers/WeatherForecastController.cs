using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly AppConfiguration _appConfiguration;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        AppConfiguration appConfiguration)
    {
        _logger = logger;
        _appConfiguration = appConfiguration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        return Ok(_appConfiguration.DistributedTracing.IsEnabled);
    }
}