using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class LoginController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        public IActionResult Login()
        {
            _log4net.Info(" Http Login request Initiated");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            _log4net.Info(" Http TokenGeneration request Initiated");
            string token="";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:51384/");
                var postData = httpclient.PostAsJsonAsync<User>("api/Authentication/AuthenicateUser", user);
              var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                       token = await res.Content.ReadAsStringAsync();
                    //string apiResponse = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if(token!=null)
                    {
                        return RedirectToAction("Index", "Booking");
                    }
                   
                }
            }
            return View("Login");
        }
    }
}