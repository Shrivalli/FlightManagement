using FlightManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.Repository
{
    public class EFFlightRepository : IFlight
    {
        public FlightManagementSystemContext context;
        public EFFlightRepository(FlightManagementSystemContext context)
        {
            this.context = context;
        }
        public IEnumerable<Flights> GetAll()
        {
            var flightlist = from flight in context.Flights select flight;
            return flightlist.ToList();
           /* List<FlightDisplayViewModel> AllFlights = new List<FlightDisplayViewModel>();
            FlightDisplayViewModel obj;
            Airlines airline;
            if (flights != null)
            {
                foreach (var flight in flights)
                {
                    airline = context.Airlines.Find(flight.AirlineId);
                    obj = new FlightDisplayViewModel() { Flights = flight, AirlineName = airline.AirlineName };
                    AllFlights.Add(obj);
                }
            }*/
        }
        public Flights FindById(int flightId)
        {
            Flights flight = context.Flights.FirstOrDefault(f=>f.FlightId == flightId);
            return flight;
           /* Airlines airline = context.Airlines.Find(flight.AirlineId);
            FlightDisplayViewModel obj = new FlightDisplayViewModel() { Flights = flight, AirlineName = airline.AirlineName };
            return obj;*/
        }

        public IEnumerable<Flights> FindByLocation(string fromlocation, string tolocation)
        {
            var flights = from flight in context.Flights where flight.FromLocation == fromlocation && flight.ToLocation == tolocation select flight;
            return flights.ToList();
           /* List<FlightDisplayViewModel> AllFlights = new List<FlightDisplayViewModel>();
            FlightDisplayViewModel obj;
            Airlines airline;
            if (flights != null)
            {
                foreach (var flight in flights)
                {
                    airline = context.Airlines.FirstOrDefault(f=>f.AirlineId == flight.AirlineId);
                    obj = new FlightDisplayViewModel() { Flights = flight, AirlineName = airline.AirlineName };
                    AllFlights.Add(obj);
                }
            }
            return AllFlights.ToList();*/
        }

        public int AddFlight([FromBody] Flights flight)
        {
            context.Flights.AddAsync(flight);
            return context.SaveChanges();
        }
        public int UpdateFlight(int id, [FromBody] Flights newflight)
        {
            Flights flight = context.Flights.FirstOrDefault(f => f.FlightId == id);
          //  context.Flights.Update(flight);
                flight.FlightId = newflight.FlightId;
                flight.AirlineId = newflight.AirlineId;
                flight.AvailableSeats = newflight.AvailableSeats;
                flight.ToLocation = newflight.ToLocation;
                flight.FromLocation = newflight.FromLocation;
                flight.Date = newflight.Date;
                flight.Price = newflight.Price;
                flight.ArrivalTime = newflight.ArrivalTime;
                flight.DepartureTime = newflight.DepartureTime;
                return context.SaveChanges();
        }
        public int DeleteFlight(int id)
        {
            Flights flight = context.Flights.FirstOrDefault(f => f.FlightId == id);
            context.Flights.Remove(flight);
            return context.SaveChanges();
        }

        
    }
}
