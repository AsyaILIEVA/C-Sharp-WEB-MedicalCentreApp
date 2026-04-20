using System.Diagnostics;
using MedicalCentreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCentreApp.Controllers
{
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error404()
        {
            return View("NotFound");
        }

        public IActionResult Error500()
        {
            return View("ServerError");
        }

        public IActionResult Error400()
        {
            return View("BadRequest");
        }

        public IActionResult HandleError(int statusCode)
        {
            return statusCode switch
            {
                400 => RedirectToAction("Error400"),
                404 => RedirectToAction("NotFound"),
                _ => RedirectToAction("ServerError")
            };
        }
    }
}
