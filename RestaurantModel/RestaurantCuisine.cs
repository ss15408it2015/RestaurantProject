using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    public class RestaurantCuisine
    {
        public int ID { get; set; }

        public int restaurantID { get; set; }
        public Restaurant restaurant { get; set; }

        public int cuisineTypeID { get; set; }
        public CuisineType cuisineType { get; set; }
    }
}   
