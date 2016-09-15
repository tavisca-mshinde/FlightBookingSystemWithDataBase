using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightIISServices.Entity;
using FlightIISServices.Model;
using System.Xml.Linq;
using System.Xml;
using System.ServiceModel;

namespace FlightIISServices.FlightServices
{
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class FlightService : IFlightService
    {

        FlightDataClassesDataContext dataContext = new FlightDataClassesDataContext(@"Data Source = SHREEKESHM\MSSQLSERVER2012; Initial Catalog = FlightBookingSystemDatabase; Persist Security Info=True;User ID = sa; Password=test123!@#");

        public Result GetFlightsBySourceDestinationTravellersAndClass(string source, string destination, string traveller, string flightClass)
        {
            Result result = new Result();
            try
            {
                if (!FlightIISServices.Validations.Validator.ValidatePositiveNumberGreaterThanZero(traveller))
                {
                    throw new Exception("Enter valid number of travellers.Travellers number should be atleast 1");
                }

                List<FlightIISServices.Entity.Flight> flightList = new List<FlightIISServices.Entity.Flight>();



                var query = dataContext.spGetFlightsBySourceDestinationTravellersAndClass(source, destination, Convert.ToInt32(traveller), flightClass);
                if (query == null)
                {
                    throw new Exception("No result found.");
                }
                result.Status = true;
                result.Message = "Flight List retrieve successfully !";
                result.FlightList = CreateFlightList(flightClass, flightList, query);
                return result;

            }
            catch (Exception ae)
            {
                result.Status = false;
                result.Message = ae.Message;
                result.FlightList = null;
                return result;
            }
            
        }

        private static List<Entity.Flight> CreateFlightList(string flightClass, List<Entity.Flight> flightList, System.Data.Linq.ISingleResult<spGetFlightsBySourceDestinationTravellersAndClassResult> query)
        {
            foreach (var q in query)
            {
                FlightIISServices.Entity.Flight flight = new FlightIISServices.Entity.Flight();
                flight.FlightId = q.FlightId;
                flight.AirlineName = q.AirlineName;
                flight.ArrivalTime = q.ArrivalTime;
                flight.DepartureTime = q.DepartureTime;
                flight.Source = q.FSource;
                flight.Destination = q.Destination;
                flight.Rating = q.Rating;
                flight.FlightClass = flightClass;
                flight.AvailableSeat = q.AvailableSeat;
                flight.Price = q.Price;
                flightList.Add(flight);
            }
            return flightList;
        }




        public string AddNewBooking(Entity.Flight flight, Entity.Customer customer,int travellers)
        {
            string bookingId = Convert.ToString(new Random().Next());
            dataContext.spAddBooking
                (customer.FisrtName+customer.LastName,
                customer.Email,
                customer.MobileNumber,
                bookingId,
                flight.FlightId,
                flight.AirlineName,
                flight.Source,
                flight.Destination,
                flight.FlightClass,
                flight.Price*travellers,
                flight.DepartureTime,
                flight.ArrivalTime,
                "Booked"
                );
            return bookingId;
        }

        public Result FilteringFlights(Result result, Filter filter)
        {
            try
            {
                result.FlightList = filter.ApplyFilter(result.FlightList);

                if (result.FlightList.Count == 0) throw new Exception("No result found.");
                return result;
            }
            catch (Exception ae)
            {
                result.Status = false;
                result.Message = ae.Message;
                result.FlightList = null;
                return result;
            }
           
        }

        public string CancelBooking(string bookindId)
        {
            dataContext.spCancelBooking(bookindId);           
            return bookindId;
        }

        public Result SaveCardDetails(Result result, Card card)
        {
            try
            {
                if (!Validations.Validator.ValidateCardNumber(card.CardNumber))
                {
                    throw new Exception("Card details is not valid. Please enter valid card number");
                }
                else if (!Validations.Validator.ValidateCVVNumber(card.CVV.ToString()))
                {
                    throw new Exception("Card CVV code is not valid. Please enter valid CVV code.");
                }
                else if (!Validations.Validator.ValidateName(card.CardHolderName))
                {
                    throw new Exception("Card holder's name is not valid. Please enter valid holder name.");
                }
                else
                {
                    dataContext.spSaveCardDetails(Convert.ToInt64(card.CardNumber), card.validTillMonthAndYear, card.CVV, card.CardHolderName);
                    return result;
                }
            }
            catch (Exception ae)
            {
                result.Status = false;
                result.Message = ae.Message;
                result.FlightList = result.FlightList;
                return result;
            }
        }
    }
}