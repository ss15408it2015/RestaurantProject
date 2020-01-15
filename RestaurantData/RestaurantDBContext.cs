using Microsoft.EntityFrameworkCore;
using RestaurantModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantData
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }
        public DbSet<RestaurantCuisine> RestaurantCuisine { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Data Source=.;Initial Catalog=ResDummyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RestaurantCuisine>().HasKey(sc => new { sc.restaurantID, sc.cuisineTypeID });
            //modelBuilder.Entity<RestaurantCuisine>().HasNoKey();

            //modelBuilder.Entity<Restaurant>().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}