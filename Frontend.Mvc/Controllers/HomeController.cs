using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Mvc.Models;

namespace Frontend.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<string> Test()
    {
        string url = "http://deployment-project-chart-backend-api:80/backend";
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(url);
        var result = await httpClient.GetAsync("WeatherForecast");
        //test
        return await result.Content.ReadAsStringAsync();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

