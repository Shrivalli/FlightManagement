using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public int NumberOfSeats { get; set; }
        public double TotalPrice { get; set; }
    }
}
