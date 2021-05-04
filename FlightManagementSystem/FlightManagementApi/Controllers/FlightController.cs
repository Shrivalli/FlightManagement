using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManagementApi.Models;
using FlightManagementApi.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace FlightManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FlightController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(FlightController));

        public IFlight repo;
        public FlightController(IFlight repo)
        {
            this.repo = repo;
        }

       [HttpGet]
       [Route("GetAllFlights")]
       public IActionResult GetAllFlights()
        {
            _log4net.Info(" Http GeAllDetails request Initiated");
            try
            {
                var list = repo.GetAll();
                if(list!=null)
                {
                    return Ok(list);
                }
                return BadRequest("No Data yet");
            }
            catch(Exception)
            {
                return BadRequest("Some Error Occured Please try again");
            }
            
        }

        [HttpGet]
        [Route("GetFlightById")]
        public IActionResult GetFlightById(int id)
        {
            _log4net.Info(" Http GetFlightById request Initiated");
            if (id==0)
            {
                return BadRequest("Enter valid id");
            }
            try
            {
                var flight = repo.FindById(id);
                if(flight!=null)
                {
                    return Ok(flight);
                }
                return BadRequest("No record");
            }
            catch(Exception)
            {
                return BadRequest("Some Error Occured Please try again");
            }
        }
     /*  [HttpGet]
        [Route("GetFlightsByLocation")]
        public IActionResult GetFlightsByLocation(string fromlocation, string tolocation)
        {
            if(fromlocation=="" || tolocation=="")
            {
                return BadRequest("Invalid Location");
            }
            try
            {
                var flightlist = repo.FindByLocation(fromlocation, tolocation);
                if(flightlist!=null)
                {
                    return Ok(flightlist);
                }
                else
                {
                    return BadRequest("No Flights Found");
                }
            }
            catch(Exception)
            {
                return BadRequest("Some Error in Connection , Please Try Again");
            }
        }*/
      
        [HttpPost]
        [Route("AddFlight")]
        public IActionResult AddFlight([FromBody]Flights flight)
        {
            _log4net.Info(" Http AddFlight request Initiated");
            if (flight==null)
            {
                return BadRequest();
            }
            try
            {
                int res = repo.AddFlight(flight);
                if(res==1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Transaction Could Not be completed");
                }
            }
            catch(Exception)
            {
                return BadRequest("Error occured during transaction.");
            }
        }

        [HttpPut]
        [Route("UpdateFlight")]
        public IActionResult UpdateFlight(int id,[FromBody]Flights newflight)
        {
            _log4net.Info(" Http UpdateFlight request Initiated");
            if (id==0 || newflight==null)
            {
                return BadRequest();
            }
            try
            {
                int res = repo.UpdateFlight(id,newflight);
                    if(res==1)
                    {
                        return Ok();
                    }
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteFlight")]
        public IActionResult Delete(int id)
        {
            _log4net.Info(" Http DeleteFlight request Initiated");
            if (id==0)
            {
                return BadRequest(); 
            }
            try
            {
                int res = repo.DeleteFlight(id);
                if (res == 1)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
       
    }
}
