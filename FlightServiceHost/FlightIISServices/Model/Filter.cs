using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace FlightIISServices.Model
{
    [DataContract]
    [KnownType(typeof(PriceFilter))]
    [KnownType(typeof(AirlineFilter))]
    [KnownType(typeof(RatingFilter))]
    
    public class Filter
    {
        [OperationContract]
        public virtual List<FlightIISServices.Entity.Flight> ApplyFilter(List<FlightIISServices.Entity.Flight> flightList)
        {
            return null;
        }
    }
}