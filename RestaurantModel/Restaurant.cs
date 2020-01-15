using System;
using System.Collections.Generic;

namespace RestaurantModel
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string locality { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public List<Rating> rating { get; set; }
        public List<RestaurantCuisine> restaurantCuisine { get; set; }
    }
}
