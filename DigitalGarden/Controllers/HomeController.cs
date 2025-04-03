using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using Microsoft.AspNetCore.Authorization;
using DigitalGarden.Filters;

namespace DigitalGarden.Controllers;

[VirtualDomFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult CareLog()
    {
        return View();
    }

    public IActionResult CommunityTips()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
