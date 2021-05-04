using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Repository
{
    public interface IBookingRepository
    {
        public int Book(int userId, int flightId, int seats);
        public int Cancel(int bookingId);
        public IEnumerable<Bookings> GetBookings(int userId);
        public IEnumerable<Flights> FindByLocation(string fromlocation, string tolocation);

    }
}
