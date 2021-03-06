using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
         //   Check();
        }
        public IActionResult Check()
        {
            try
            {
                if (TempData["token"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                return RedirectToAction("Index");
            }
            catch(NullReferenceException)
            {
                return RedirectToAction("Login", "Login");
            }
            catch(Exception)
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        public IActionResult Index()
        {
            return View();
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
}
