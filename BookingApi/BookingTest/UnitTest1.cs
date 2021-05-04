using BookingApi.Controllers;
using BookingApi.Models;
using BookingApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BookingTest
{
    public class Tests
    {
        EFDbContext db;
        [SetUp]
        public void Setup()
        {
            var bookinglist = new List<Bookings>
            { 
                new Bookings{ BookingId=1,UserId=1,FlightId=1,NumberOfSeats=1,TotalPrice=1000},
                new Bookings{ BookingId=2,UserId=2,FlightId=1,NumberOfSeats=1,TotalPrice=2000},
                new Bookings{ BookingId=3,UserId=3,FlightId=1,NumberOfSeats=1,TotalPrice=1000},

            };

          
            var bookings = bookinglist.AsQueryable();
            var mockSet = new Mock<DbSet<Bookings>>();
            mockSet.As<IQueryable<Bookings>>().Setup(m => m.Provider).Returns(bookings.Provider);
            mockSet.As<IQueryable<Bookings>>().Setup(m => m.Expression).Returns(bookings.Expression);
            mockSet.As<IQueryable<Bookings>>().Setup(m => m.ElementType).Returns(bookings.ElementType);
            mockSet.As<IQueryable<Bookings>>().Setup(m => m.GetEnumerator()).Returns(bookings.GetEnumerator());
            var p = new DbContextOptions<EFDbContext>();
            var mockContext = new Mock<EFDbContext>(p);
            mockContext.Setup(c => c.Bookings).Returns(mockSet.Object);
            db = mockContext.Object;

        }

        [Test]
        public void GetTicket_Valid_OkRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            var d1 = h2.GetTickets(1);
            var R1 = d1 as OkObjectResult;
            Assert.AreEqual(200, R1.StatusCode);
        }

        [Test]
        public void GetTicket_InValid_BadRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            var d1 = h2.GetTickets(0);
            Assert.IsNotNull(d1);
        }

        [Test]
        public void BookTicket_Valid_OkRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            Bookings ticket = new Bookings { BookingId=3,FlightId=1,UserId=1,NumberOfSeats=1,TotalPrice=1000};
            var d1 = h2.BookTicket(ticket);
            Assert.IsNotNull(d1);
        }
        [Test]
        public void BookTicket_InValid_BadRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            Bookings ticket = new Bookings { BookingId = 1};
            var d1 = h2.BookTicket(ticket);
            var R1 = d1 as ObjectResult;
            Assert.AreEqual(400, R1.StatusCode);
        }
        [Test]
        public void CancelTicket_Valid_OkRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            var d1 = h2.CancelBooking(1);
            Assert.IsNotNull(d1);
        }
        [Test]
        public void CancelTicket_InValid_BadRequest()
        {
            var res = new Mock<EFBookingRepository>(db);
            BookingController h2 = new BookingController(res.Object);
            var d1 = h2.CancelBooking(0);
            var R1 = d1 as ObjectResult;
            Assert.AreEqual(400, R1.StatusCode);
        }




    }
}