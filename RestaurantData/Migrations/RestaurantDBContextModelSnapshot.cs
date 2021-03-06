﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantData;

namespace RestaurantData.Migrations
{
    [DbContext(typeof(RestaurantDBContext))]
    partial class RestaurantDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RestaurantModel.CuisineType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("cuisineType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CuisineTypes");
                });

            modelBuilder.Entity("RestaurantModel.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<int>("restaurantID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("restaurantID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("RestaurantModel.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("lng")
                        .HasColumnType("real");

                    b.Property<string>("locality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("RestaurantModel.RestaurantCuisine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("cuisineTypeID")
                        .HasColumnType("int");

                    b.Property<int>("restaurantID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("cuisineTypeID");

                    b.HasIndex("restaurantID");

                    b.ToTable("RestaurantCuisine");
                });

            modelBuilder.Entity("RestaurantModel.Rating", b =>
                {
                    b.HasOne("RestaurantModel.Restaurant", "restaurant")
                        .WithMany("rating")
                        .HasForeignKey("restaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantModel.RestaurantCuisine", b =>
                {
                    b.HasOne("RestaurantModel.CuisineType", "cuisineType")
                        .WithMany("restaurantCuisine")
                        .HasForeignKey("cuisineTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantModel.Restaurant", "restaurant")
                        .WithMany("restaurantCuisine")
                        .HasForeignKey("restaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
