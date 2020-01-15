using AutoMapper;
using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Helper
{
    public static class ConvertEntityToDto
    {
        //public static List<int> getIntRatingList(this List<Rating> ratingObject)
        //{
        //    List<int> ratingList = new List<int>();
        //    foreach(var rating in ratingObject)
        //    {
        //        ratingList.Add(rating.rating);
        //    }
        //    return ratingList;
        //}

        public static List<CuisineTypeDto> getCuisineTypeList(this List<RestaurantCuisine> resCuisine)
        {
            List<CuisineTypeDto> cuisineTypeList = new List<CuisineTypeDto>();
            foreach (var resCuisineType in resCuisine)
            {
                CuisineTypeDto cuisineTypeObj = new CuisineTypeDto();
                cuisineTypeObj.cuisineID = resCuisineType.cuisineType.ID;
                cuisineTypeObj.cuisineType = resCuisineType.cuisineType.cuisineType;
                cuisineTypeList.Add(cuisineTypeObj);
            }
            return cuisineTypeList;
        }
    }
}
