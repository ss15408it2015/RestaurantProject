using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RestaurantModel
{
    public class CuisineType
    {
        public int ID { get; set; }
        public string cuisineType { get; set; }
        public List<RestaurantCuisine> restaurantCuisine { get; set; }
    }
}
