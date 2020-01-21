using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantData
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetRestaurantByID(int ID);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Restaurant AddRestaurant(Restaurant restaurant);
        Task RemoveRestaurant(int ID);
        Restaurant UpdateRestaurant(Restaurant newRestaurant);


        Task<bool> RestaurantExist(int restaurantID);
        Task<bool> CuisineTypeExist(int cuisineID);
        Task<bool> RestaurantHasCuisineType(int restaurantID, int cuisineID);


        Task<Rating> GetSingleRating(int restaurantID, int ratingID);
        Task<IEnumerable<Rating>> GetAllRatings(int restaurantID);
        Task<Rating> AddSingleRating(int restaurantID, Rating rating);
        void RemoveSingleRating(int restaurantID, int ratingID);
        Rating UpdateRating(int restaurantID, int ratingID, Rating rating);


        Task<CuisineType> GetSingleCuisineType(int cuisineTypeID);
        Task<IEnumerable<CuisineType>> GetAllCuisineTypes();
        Task<CuisineType> AddCuisineType(CuisineType cuisineType);
        void RemoveSingleCuisineType(int cuisineTypeID);
        CuisineType UpdateCuisineType(CuisineType cuisineTypeEntity);


        Task<IEnumerable<CuisineType>> GetCuisineTypeOfRestaurant(int restaurantID);
        Task<RestaurantCuisine> AddCuisineTypeToRestaurant(RestaurantCuisine resCuisineType);
        void RemoveCuisineTypeFromRestaurant(int restaurantID, int cuisineID);


        Task<int> SaveAsync();
    }
}
