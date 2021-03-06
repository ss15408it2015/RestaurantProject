﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel
{
    /// <summary>
    /// An Restaurant with id, name, street, locality, city, state, country, postal, lat, lang, List of RatingDto, List of CuisineTypeDto
    /// </summary>
    public class RestaurantDto
    {
        /// <summary>
        /// Id of Restaurant
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of Restaurant
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Street of restaurant
        /// </summary>
        public string street { get; set; }
        
        /// <summary>
        /// locality of restaurant
        /// </summary>
        public string locality { get; set; }
        
        /// <summary>
        /// city of restaurant
        /// </summary>
        public string city { get; set; }
        
        /// <summary>
        /// state of restaurant
        /// </summary>
        public string state { get; set; }
        
        /// <summary>
        /// country of restaurant
        /// </summary>
        public string country { get; set; }
        
        /// <summary>
        /// postal of restaurant
        /// </summary>
        public string postal { get; set; }
        
        /// <summary>
        /// latitude of restaurant
        /// </summary>
        public float lat { get; set; }
        
        /// <summary>
        /// longitude of restaurant
        /// </summary>
        public float lng { get; set; }
        
        /// <summary>
        /// List of Ratings of restaurant
        /// </summary>
        public List<RatingDto> rating { get; set; }
        
        /// <summary>
        /// List of cuisine tpye of restaurant
        /// </summary>
        public List<CuisineTypeDto> cuisineType { get; set; }
    }
}
