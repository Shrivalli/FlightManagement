using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Models
{
    public partial class Bookings
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
