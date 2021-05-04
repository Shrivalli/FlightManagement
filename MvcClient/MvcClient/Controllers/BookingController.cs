using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcClient.Models;
using Newtonsoft.Json;

namespace MvcClient.Controllers
{
    public class BookingController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
        public IActionResult Index()
        {
            TempData["UserId"] = 1;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FetchByLocation location)
        {
            _log4net.Info(" Http GetFlightsById request Initiated");
            var list = new List<Flights>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:50183/");
                HttpResponseMessage res = await httpclient.GetAsync("api/Booking/GetFlightsByLocation?fromlocation="+location.FromLocation+ "&tolocation="+location.ToLocation);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Flights>>(result);
                }
            }
            TempData["flights"] = JsonConvert.SerializeObject(list);
            return RedirectToAction("ShowFlights");
        }

        [HttpGet]
        public IActionResult ShowFlights()
        {  
           var lst =  JsonConvert.DeserializeObject<List<Flights>>(TempData["flights"].ToString());
            if(lst.Count==0)
            {
                return RedirectToAction("Index");
            }
            return View(lst);
        }

        public async Task<IActionResult> GetTickets()
        {
            _log4net.Info(" Http GetBookedTickets request Initiated");
            TempData["UserId"] = 1;
            ViewBag.Name = "abc";
            var list = new List<Bookings>();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:50183/");
                HttpResponseMessage res = await httpclient.GetAsync("api/Booking/GetTickets?userId="+TempData["UserId"]);
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Bookings>>(result);
                }
            }
            return View(list);
        }

        public IActionResult BookTicket(int id)
        {
            _log4net.Info(" Http BookTicket request Initiated");
            int userid = (int)TempData["UserId"];
            Bookings bookings = new Bookings() { BookingId=0,UserId=userid,FlightId=id,NumberOfSeats=1,TotalPrice=0};
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:50183/");
             //   var data = httpclient.PostAsync("api/Booking/BookTicket?userid=" + userid + "&flightid=" + fid + "&seats=1");
                var postData = httpclient.PostAsJsonAsync<Bookings>("api/Booking/BookTicket",bookings);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    return View("Booked");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Booked()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            _log4net.Info(" Http CancelBooking request Initiated");
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:50183/");
                var delete = httpclient.DeleteAsync("api/Booking/CancelBooking?bookingid=" + id);
                var res = delete.Result;
                return RedirectToAction("GetTickets");
            }
        }
    }
}