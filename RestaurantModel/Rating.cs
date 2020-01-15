using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    public class Rating
    {
        public int ID { get; set; }
        public int rating { get; set; }

        public int restaurantID { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
