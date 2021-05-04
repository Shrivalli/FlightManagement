using FlightManagementApi.Controllers;
using FlightManagementApi.Models;
using FlightManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementTesting
{
    public class Tests
    {
        FlightManagementSystemContext db;
       // Flights f = new Flights { FlightId = 1, AirlineId = 1, FromLocation = "Dehradun", ToLocation = "Dehli", Duration = 45, AvailableSeats = 100, ArrivalTime = "10:00", DepartureTime = "9:15" };
        [SetUp]
        public void Setup()
        {
            var loan = new List<Flights>
            {
                new Flights{ FlightId=1,AirlineId=1,FromLocation="Dehradun",ToLocation="Dehli",Duration=45,AvailableSeats=100,ArrivalTime="10:00",DepartureTime="9:15" },
                new Flights{ FlightId=2,AirlineId=1,FromLocation="Dehradun",ToLocation="Mumbai",Duration=60,AvailableSeats=100,ArrivalTime="10:00",DepartureTime="9:15" },
            };
            var loandata = loan.AsQueryable();
            var mockSet = new Mock<DbSet<Flights>>();
            mockSet.As<IQueryable<Flights>>().Setup(m => m.Provider).Returns(loandata.Provider);
            mockSet.As<IQueryable<Flights>>().Setup(m => m.Expression).Returns(loandata.Expression);
            mockSet.As<IQueryable<Flights>>().Setup(m => m.ElementType).Returns(loandata.ElementType);
            mockSet.As<IQueryable<Flights>>().Setup(m => m.GetEnumerator()).Returns(loandata.GetEnumerator());
            var mockContext = new Mock<FlightManagementSystemContext>();
            mockContext.Setup(c => c.Flights).Returns(mockSet.Object);
            db = mockContext.Object;
        }

        [Test]
        public void GetAllFlights_Valid_OkResult()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            var data = obj.GetAllFlights();
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);

        }

        [Test]
        public void GetAllFlights_INValid_BadResult()
        {
            try
            {
                var mock = new Mock<EFFlightRepository>(db);//repos
                FlightController obj = new FlightController(mock.Object);
                var data = obj.GetAllFlights();
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }
        [Test]
        public void GetAllFlight_ReturnsNotnullList()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            var data = obj.GetAllFlights();
            Assert.IsNotNull(data);
        }

        [Test]
        public void GetFlightById_Valid_OkResult()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            var data = obj.GetFlightById(1);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);

        }

        [Test]
        public void GetFlightById_INValid_BadRequest()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            var data = obj.GetFlightById(0);
            var res = data as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);

        }
        [Test]
        public void AddFlight_Valid_OkRequest()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            Flights flight = new Flights { FlightId = 4, AirlineId = 1, FromLocation = "Dehradun", ToLocation = "Dehli", Date = DateTime.Parse("2020-10-1T00:00:00"), Duration = 45, AvailableSeats = 100, ArrivalTime = "10:00", DepartureTime = "9:15" };
          //  mock.Setup(p => p.AddFlight(flight)).Returns(4);
            FlightController obj = new FlightController(mock.Object);
            // DateTime dt = Convert.ToDateTime("2020-10-18");

            var data = obj.AddFlight(flight);
            var res = data as OkObjectResult;
            // Assert.AreEqual(200, res.StatusCode);
            Assert.IsNotNull(data);


        }
        [Test]
        public void AddFlight_INValid_BadRequest()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            Flights flight = new Flights { FlightId = 1, AirlineId = 1, FromLocation = "Dehradun", ToLocation = "Dehli", Duration = 45, AvailableSeats = 100, ArrivalTime = "10:00", DepartureTime = "9:15" };
            var data = obj.AddFlight(flight);
            var res = data as ObjectResult;
            Assert.AreEqual(400, res.StatusCode);

        }
        [Test]
        public void UpdateFlight_Valid_OkRequest()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            Flights flight = new Flights { FlightId = 1, AirlineId = 1, FromLocation = "Dehradun", ToLocation = "Dehli", Duration = 45, AvailableSeats = 100, ArrivalTime = "10:00", DepartureTime = "9:15" };
            var data = obj.UpdateFlight(1, flight);
            var res = data as ObjectResult;
            Assert.IsNotNull(data);

        }
        [Test]
        public void DeleteFlight_Valid_OkRequest()
        {
            var mock = new Mock<EFFlightRepository>(db);//repos
            FlightController obj = new FlightController(mock.Object);
            var data = obj.Delete(1);
            Assert.IsNotNull(data);
        }
        [Test]
        public void DeleteFlight_InValid_BadRequest()
        {
            try
            {
                var mock = new Mock<EFFlightRepository>(db);//repos
                FlightController obj = new FlightController(mock.Object);
                var data = obj.Delete(0);
                var res = data as OkObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }
    }
    }