using Microsoft.EntityFrameworkCore;
using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantData
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDBContext _context;

        public RestaurantRepository(RestaurantDBContext restaurantDBContext)
        {
            _context = restaurantDBContext;
        }


        public async Task<Restaurant> GetRestaurantByID(int ID)
        {
            return await _context.Restaurants.Where(x => x.ID == ID)
                                        .Include(x => x.rating)
                                        .Include(r => r.restaurantCuisine)
                                        .ThenInclude(c => c.cuisineType)
                                        .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _context.Restaurants
                                        .Include(r => r.rating)
                                        .Include(r => r.restaurantCuisine)
                                        .ThenInclude(c => c.cuisineType)
                                        .ToListAsync();
        }
        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            return restaurant;
        }
        public void RemoveRestaurant(int ID)
        {
            Restaurant restaurant = _context.Restaurants.Find(ID);
            if (restaurant != null)
                _context.Restaurants.Remove(restaurant);
        }
        public Restaurant UpdateRestaurant(Restaurant newRestaurant)
        {
            _context.Restaurants.Update(newRestaurant);

            return newRestaurant;
        }




        public async Task<bool> RestaurantExist(int restaurantID)
        {
            return await _context.Restaurants.AnyAsync(a => a.ID == restaurantID);
        }
        public async Task<bool> CuisineTypeExist(int cuisineID)
        {
            return await _context.CuisineTypes.AnyAsync(a => a.ID == cuisineID);
        }
        public async Task<bool> RestaurantHasCuisineType(int restaurantID, int cuisineID)
        {
            return await _context.RestaurantCuisine
                                    .AnyAsync(i => 
                                    (
                                        i.restaurantID == restaurantID
                                        &&
                                        i.cuisineTypeID == cuisineID
                                    ));
        }



        public async Task<Rating> GetSingleRating(int restaurantID, int ratingID)
        {
            return await _context.Ratings.Where(i => (i.ID == ratingID) && (i.restaurantID == restaurantID)).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Rating>> GetAllRatings(int resID)
        {
            return await _context.Ratings.Where(i => i.restaurantID == resID).ToListAsync();
        }
        public Rating AddSingleRating(int restaurantID, Rating rating)
        {
            rating.restaurant = null;
            _context.Ratings.Add(rating);
           
            return rating;
        }
        public void RemoveSingleRating(int restaurantID, int ratingID)
        {
            Rating rating = _context.Ratings.Where(i => (i.ID == ratingID) && (i.restaurantID == restaurantID)).FirstOrDefault();
            if (rating != null)
                _context.Ratings.Remove(rating);
        }
        public Rating UpdateRating(Rating newRating)
        {
            _context.Ratings.Update(newRating);
            
            return newRating;
        }



        public async Task<CuisineType> GetSingleCuisineType(int cuisineTypeID)
        {
            return await _context.CuisineTypes.FindAsync(cuisineTypeID);
        }
        public async Task<IEnumerable<CuisineType>> GetAllCuisineTypes()
        {
            return await _context.CuisineTypes.ToListAsync();
        }
        public async Task<CuisineType> AddCuisineType(CuisineType cuisineType)
        {
            var cusine = await _context.CuisineTypes.Where(t => t.cuisineType == cuisineType.cuisineType).FirstOrDefaultAsync();

            if (cusine != null)
                return cusine;
            
            _context.CuisineTypes.Add(cuisineType);
            return cuisineType;
        }
        public void RemoveSingleCuisineType(int cuisineTypeID)
        {
            CuisineType cuisineType = _context.CuisineTypes.Find(cuisineTypeID);
            if (cuisineType != null)
                _context.CuisineTypes.Remove(cuisineType);
        }
        public CuisineType UpdateCuisineType(CuisineType cuisineType)
        {
            _context.CuisineTypes.Update(cuisineType);

            return cuisineType;
        }



        public async Task<IEnumerable<CuisineType>> GetCuisineTypeOfRestaurant(int restaurantID)
        {
            var cuisineTypeIDs = await _context.RestaurantCuisine.Where(i => i.restaurantID == restaurantID)
                                                                    .Select(c => c.cuisineTypeID)
                                                                    .ToListAsync();

            return await _context.CuisineTypes.Where(i => cuisineTypeIDs.Contains(i.ID)).ToListAsync();
        }
        public async Task<RestaurantCuisine> AddCuisineTypeToRestaurant(RestaurantCuisine resCuisineType)
        {
            var resCuisine = await _context.RestaurantCuisine
                                        .Where(t => (t.cuisineTypeID == resCuisineType.cuisineTypeID) 
                                                    && 
                                                    (t.restaurantID == resCuisineType.restaurantID))
                                        .Include(t => t.cuisineType)
                                        .FirstOrDefaultAsync();
            
            if (resCuisine != null)
                return resCuisine;

            resCuisineType.cuisineType = await _context.CuisineTypes.FindAsync(resCuisineType.cuisineTypeID);
            _context.RestaurantCuisine.Add(resCuisineType);
            return resCuisineType;
        }
        public void RemoveCuisineTypeFromRestaurant(int restaurantID, int cuisineID)
        {
            var resCuisine = _context.RestaurantCuisine
                                        .Where(i => (i.restaurantID == restaurantID) 
                                                    && 
                                                    (i.cuisineTypeID == cuisineID))
                                        .FirstOrDefault();
            if (resCuisine != null)
                _context.RestaurantCuisine.Remove(resCuisine);
        }



        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        ~RestaurantRepository()
        {
            _context.Dispose();
        }
    }
}