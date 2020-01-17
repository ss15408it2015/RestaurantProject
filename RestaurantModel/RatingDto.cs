using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    /// <summary>
    /// Rating with id and rating.
    /// </summary>
    public class RatingDto
    {
        /// <summary>
        /// ratingID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// rating of restaurant
        /// </summary>
        public int rating { get; set; }
    }
}
