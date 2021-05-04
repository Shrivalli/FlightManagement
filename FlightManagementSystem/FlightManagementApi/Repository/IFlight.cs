using FlightManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repository
{
    public interface IFlight
    {
        public IEnumerable<Flights> GetAll();
        public Flights FindById(int flightId);
        public IEnumerable<Flights> FindByLocation(string fromlocation, string tolocation);
        public int AddFlight([FromBody]Flights flight);
        public int UpdateFlight(int id, [FromBody]Flights newflight);
        public int DeleteFlight(int id);



    }
}
