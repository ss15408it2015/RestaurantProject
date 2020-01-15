using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Helper
{
    public static class ConvertDtoToEntity
    {
        public static List<RestaurantCuisine> getRestaurantCuisineEntityAsList(this List<CuisineTypeDto> cuisineTypeDto)
        {
            List<RestaurantCuisine> restaurantCuisine = new List<RestaurantCuisine>();
            foreach (var cuisineTypeDetail in cuisineTypeDto)
            {
                RestaurantCuisine resCuisine = new RestaurantCuisine();
                resCuisine.cuisineTypeID = cuisineTypeDetail.cuisineID;
                
                restaurantCuisine.Add(resCuisine);
            }
            return restaurantCuisine;
        }

        public static List<Rating> getRatingEntity(this List<RatingDto> ratingDtoObject)
        {
            List<Rating> ratingEntity = new List<Rating>();
            foreach(var ratingDto in ratingDtoObject)
            {
                Rating rating = new Rating();
                rating.rating = ratingDto.rating;

                ratingEntity.Add(rating);
            }
            return ratingEntity;
        }
    }
}
