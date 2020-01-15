using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    public class RestaurantDto
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
        public List<RatingDto> rating { get; set; }
        public List<CuisineTypeDto> cuisineType { get; set; }
    }
}
