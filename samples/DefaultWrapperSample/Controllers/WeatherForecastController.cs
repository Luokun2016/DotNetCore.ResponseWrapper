using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection.Metadata;
using System.Web;

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

    [HttpGet("ok")]
    public IActionResult Ok1()
    {
        return Ok();
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

    [HttpGet("download-file")]
    public IActionResult DownloadTemplate()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Test.xlsx");
        var fileExt = Path.GetExtension(filePath);
        var provider = new FileExtensionContentTypeProvider();
        var contentType = provider.Mappings[fileExt];
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var result = new Tuple<string, string, FileStream>($"Test.xlsx", contentType, fs);
        return File(result.Item3, result.Item2, HttpUtility.UrlEncode(result.Item1));
    }
}