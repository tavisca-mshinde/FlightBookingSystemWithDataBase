using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FlightIISServices.Model
{
    [DataContract]
    public class AirlineFilter : Filter
    {
        [DataMember]
        public string AirlineName { get; set; }
        public AirlineFilter(string airlineName)
        {
            AirlineName = airlineName;
        }

        public override List<FlightIISServices.Entity.Flight> ApplyFilter(List<FlightIISServices.Entity.Flight> flightList)
        {
            var query = from d in flightList where d.AirlineName.Equals(AirlineName) select d;
            return query.ToList();
        }

    }
}