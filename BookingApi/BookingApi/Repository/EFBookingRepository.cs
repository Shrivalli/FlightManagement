using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Repository
{
    public class EFBookingRepository : IBookingRepository
    {
        public EFDbContext context;
        public EFBookingRepository(EFDbContext context)
        {
            this.context = context;
        }
        public int Book(int userId, int flightId, int seats)
        {
            int res = 0;
            var flight = context.Flights.Find(flightId);
            if (flight != null)
            {
                double price = seats * (double)flight.Price;
                Bookings obj = new Bookings() { UserId = userId, FlightId = flightId, NumberOfSeats = seats, TotalPrice = (decimal)price };
                context.Bookings.Add(obj);
                context.SaveChanges();

                flight.AvailableSeats = flight.AvailableSeats - seats;
                context.Flights.Update(flight);
                res = context.SaveChanges();

            }
            return res;
        }

        public int Cancel(int bookingId)
        {
            int res = 0;
            var booking = context.Bookings.Find(bookingId);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                context.SaveChanges();
                var flight = context.Flights.FirstOrDefault(f=>f.FlightId == booking.FlightId);
                flight.AvailableSeats = flight.AvailableSeats + booking.NumberOfSeats;
                context.Flights.Update(flight);
                res = context.SaveChanges();
            }
            return res;
        }

        public IEnumerable<Bookings> GetBookings(int userId)
        {
            var tickets = from booking in context.Bookings where booking.UserId == userId select booking;
            return tickets.ToList();
        }

        public IEnumerable<Flights> FindByLocation(string fromlocation, string tolocation)
        {
            var flights = from flight in context.Flights where flight.FromLocation == fromlocation && flight.ToLocation == tolocation select flight;
            return flights.ToList();
        }

    }
}
