using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightIISServices.Model
{
    public class RatingFilter : Filter
    {
        public double Rating { set; get; }

        public RatingFilter(double rating)
        {
            Rating =rating;
        }
        public override List<FlightIISServices.Entity.Flight> ApplyFilter(List<FlightIISServices.Entity.Flight> flightList)
        {
            var query = from d in flightList where d.Rating >= Rating select d;
            return query.ToList();
        }
    }
}