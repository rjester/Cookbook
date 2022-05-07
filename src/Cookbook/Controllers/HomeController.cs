using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cookbook.Models;
using Microsoft.AspNetCore.Authorization;
using Cookbook.Infrastructure.Services;

namespace Cookbook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRecipeService _recipeSvc;
    public HomeController(ILogger<HomeController> logger, IRecipeService recipeSvc)
    {
        _logger = logger;
        _recipeSvc = recipeSvc;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        var detail = _recipeSvc.GetDetail("test");
        return View();
    }

    [Authorize]
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
