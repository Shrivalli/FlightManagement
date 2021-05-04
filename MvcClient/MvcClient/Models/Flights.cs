using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class Flights
    {
        [Key]
        public int FlightId { get; set; }
        public int AirlineId { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
    }
}
