using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace FlightIISServices.Model
{
    [Serializable]
    [DataContract]
    public class PriceFilter : Filter
    {
        [DataMember]
        public int StartRange { set; get; }
        [DataMember]
        public int EndRange { get; set; }
        
       public PriceFilter(int startRange, int endRange)
        {
            StartRange = startRange;
            EndRange = endRange;
        }
        
        public override List<FlightIISServices.Entity.Flight> ApplyFilter(List<FlightIISServices.Entity.Flight> flightList)
        {
            var query = from d in flightList where d.Price >= StartRange && d.Price <= EndRange select d;
            return query.ToList();
        }
    }
}