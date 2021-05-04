using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingApi.Models;
using BookingApi.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BookingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
        public IBookingRepository repo;
        public BookingController(IBookingRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("GetFlightsByLocation")]
        public IActionResult GetFlightsByLocation(string fromlocation, string tolocation)
        {
            _log4net.Info(" Http GetFlightsByLocation request Initiated");
            if (fromlocation == "" || tolocation == "")
            {
                return BadRequest("Invalid Location");
            }
            try
            {
                var flightlist = repo.FindByLocation(fromlocation, tolocation);
                if (flightlist != null)
                {
                    return Ok(flightlist);
                }
                else
                {
                    return BadRequest("No Flights Found");
                }
            }
            catch (Exception)
            {
                return BadRequest("Some Error in Connection , Please Try Again");
            }
        }

        [HttpGet]
        [Route("GetTickets")]
        public IActionResult GetTickets(int userId)
        {
            _log4net.Info(" Http GetTicket request Initiated");
            if (userId==0)
            {
                return BadRequest("Provide Valid User Id");
            }
            try
            {
                var tickets = repo.GetBookings(userId);
                if(tickets!=null)
                {
                    return Ok(tickets);
                }
                return BadRequest("No Booking yet");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpPost]
        [Route("BookTicket")]
        public IActionResult BookTicket(Bookings book)
        {
            _log4net.Info(" Http BookTicket request Initiated");
            if (book.UserId == 0 || book.FlightId == 0 || book.NumberOfSeats == 0)
            {
                return BadRequest("Please provide valid input");
            }

            try
            {
                var res = repo.Book(book.UserId, book.FlightId,book.NumberOfSeats);
                if (res == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("CancelBooking")]
        public IActionResult CancelBooking(int bookingid)
        {
            _log4net.Info(" Http CancelBooking request Initiated");
            if (bookingid == 0)
            {
                return BadRequest("Provide Correct Input");
            }
            try
            {
                var res = repo.Cancel(bookingid);
                if (res == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not be cancelled please try again");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}