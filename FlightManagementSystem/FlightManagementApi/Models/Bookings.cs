using System;
using System.Collections.Generic;

namespace FlightManagementApi.Models
{
    public partial class Bookings
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
