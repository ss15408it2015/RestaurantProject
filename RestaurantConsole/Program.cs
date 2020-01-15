using RestaurantData;
using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int resID;
            RestaurantRepository repo = new RestaurantRepository();

            //repo.L(5);

            //Add Method
            //repo.Add(RestaurantDummyData());


            //Remove Method
            //resID = 3;
            //if (repo.Remove(resID))
            //    Console.WriteLine("Retaurant is Deleted");
            //else
            //    Console.WriteLine("Restaurant not found");


            //GetByID Method
            //resID = 5;
            //Restaurant restaurant = repo.GetByID(resID);
            //Console.Write("Id : " + restaurant.ID + "\nName : " + restaurant.name + "\nRating : ");

            //foreach (var ratings in restaurant.rating)
            //    Console.Write(ratings.rating + "\t");

            //Console.Write("\nCuisine Type : ");
            //foreach (var resCuisine in restaurant.restaurantCuisine)
            //    Console.Write(resCuisine.cuisineType.cuisineType + "\t");

            //Console.WriteLine("\n\n");


            //GetAll Method
            List<Restaurant> restaurantList = repo.GetAllRestaurants();
            foreach (var res in restaurantList)
            {
                Console.Write("Id : " + res.ID + "\nName : " + res.name + "\nRating : ");

                foreach (var ratings in res.rating)
                    Console.Write(ratings.rating + "\t");

                Console.Write("\nCuisine Type : ");
                foreach (var resCuisine in res.restaurantCuisine)
                    Console.Write(resCuisine.cuisineType.cuisineType + "\t");

                Console.WriteLine("\n\n");
            }

        }

        public static List<CuisineType> CuisineData()
        {
            return new List<CuisineType>()
            {
                new CuisineType()
                {
                    cuisineType = "Indian"
                },
                new CuisineType()
                {
                    cuisineType = "Chinese"
                }
            };
        }

        public static Restaurant RestaurantDummyData()
        {
           return new Restaurant()
            {
                name = "Madhu Shree",
                street = "5th Main Cross",
                locality = "Talwandi",
                city = "Kota",
                state = "Rajasthan",
                country = "India",
                postal = "324005",
                lat = 23.435f,
                lng = 23.433f,
                rating = new List<Rating>()
                {
                    new Rating()
                    {
                        rating = 2
                    },
                    new Rating()
                    {
                        rating = 1
                    }
                },
                restaurantCuisine = new List<RestaurantCuisine>()
                {
                    new RestaurantCuisine()
                    {
                       cuisineTypeID = 8
                    }
                }
            };

            
        }

        public static List<RestaurantCuisine> RestaurantCuisineDummyData()
        {
            return new List<RestaurantCuisine>()
            {
                new RestaurantCuisine()
                {
                    restaurantID = 4,
                    cuisineTypeID = 7
                },
                new RestaurantCuisine()
                {
                    restaurantID = 5,
                    cuisineTypeID = 7
                }
            };
        }
    }
}
