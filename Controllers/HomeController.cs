using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CapSystemFinal.Models;

namespace CapSystemFinal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole(SD.Role_Staff))
            {
                return RedirectToAction("Index", "Complaints");
            }
            if (User.IsInRole(SD.Role_Student))
            {
                return RedirectToAction("Index", "ComplaintStudents");
            }
            if (User.IsInRole(SD.Role_Dean))
            {
                return RedirectToAction("Index", "Deans");
            }

        }
        return LocalRedirect("~/Identity/Account/Login");
        //return View("Index");
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
