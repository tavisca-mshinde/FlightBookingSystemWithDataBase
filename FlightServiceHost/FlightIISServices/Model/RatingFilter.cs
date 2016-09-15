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
    public class RatingFilter : Filter
    {
        [DataMember]
        public double Rating { set; get; }

        public RatingFilter(double rating)
        {
            Rating =rating;
        }
        [OperationContract]
        public override List<FlightIISServices.Entity.Flight> ApplyFilter(List<FlightIISServices.Entity.Flight> flightList)
        {
            var query = from d in flightList where d.Rating >= Rating select d;
            return query.ToList();
        }
    }
}