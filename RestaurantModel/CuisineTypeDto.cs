using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    /// <summary>
    /// CuisineType with id and cuisineType
    /// </summary>
    public class CuisineTypeDto
    {
        /// <summary>
        /// cuisineID of cuisineType
        /// </summary>
        public int cuisineID { get; set; }

        /// <summary>
        /// name of cuisineType 
        /// </summary>
        public string cuisineType { get; set; }
    }
}
