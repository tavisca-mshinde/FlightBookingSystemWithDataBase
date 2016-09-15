using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightIISServices;
using FlightIISServices.Model;
using FlightIISServices.Entity;

namespace FlightIIsServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        FlightIISServices.FlightServices.FlightService flightService = new FlightIISServices.FlightServices.FlightService();
        [TestMethod]
        public void TestForGetFlightsBySourceDestinationTravellersAndClassWithValidInput()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            Assert.IsTrue(result.Status);
        }

        [TestMethod]
        public void TestForGetFlightsBySourceDestinationTravellersAndClassWithInvalidTraveller()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "-2", "Economy");
            Assert.IsFalse(result.Status);
        }

        //[TestMethod]
        //public void TestForGetFlightsBySourceDestinationTravellersAndClassForNoResultFound()
        //{
        //    Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Delhi", "2", "Economy");
        //    Assert.IsFalse(result.Status);
        //}

        [TestMethod]
        public void TestFilteringFlightsWithPriceFilter()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            result = flightService.FilteringFlights(result, new PriceFilter(3000, 3000));
            Assert.IsTrue(result.Status);

        }

        [TestMethod]
        public void TestFilteringFlightsWithAirlineFilter()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            result = flightService.FilteringFlights(result, new AirlineFilter("Vistara"));
            Assert.IsTrue(result.Status);

        }

        [TestMethod]
        public void TestFilteringFlightsWithRatingFilter()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            result = flightService.FilteringFlights(result, new RatingFilter(3.5));
            Assert.IsTrue(result.Status);

        }
        [TestMethod]
        public void TestAddNewBooking()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");

            string bookingId = flightService.AddNewBooking(result.FlightList[0], new FlightIISServices.Entity.Customer { FisrtName = "Mayuresh", LastName = "Bhanushali", Email = "mb@tv.com", MobileNumber = "9854659887" }, 2);
            Assert.IsNotNull(bookingId);
        }
        [TestMethod]
        public void TestSaveCardDetailsWithValidDetails()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            result = flightService.SaveCardDetails(result, new FlightIISServices.Entity.Card { CardNumber = "1234567894561230", validTillMonthAndYear = "09/20", CVV = 420, CardHolderName = "Mayuresh Bhanushali" });
            Assert.IsTrue(result.Status);
        }
        [TestMethod]
        public void TestSaveCardDetailsWithInvalidDetails()
        {
            Result result = flightService.GetFlightsBySourceDestinationTravellersAndClass("Pune", "Mumbai", "2", "Economy");
            result = flightService.SaveCardDetails(result, new FlightIISServices.Entity.Card { CardNumber = "12345678945630", validTillMonthAndYear = "09/20", CVV = 420, CardHolderName = "Mayuresh Bhanushali" });
            Assert.IsFalse(result.Status);
        }

        [TestMethod]
        public void TestCancelBooking()
        {

            string bookingId = flightService.CancelBooking("984446672");
            Assert.IsNotNull(bookingId);
        }

    }
}
