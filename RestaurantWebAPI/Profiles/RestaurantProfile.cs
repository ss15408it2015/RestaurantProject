using AutoMapper;
using RestaurantWebAPI.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(
                    dest => dest.cuisineType,
                    opt => opt.MapFrom(src => src.restaurantCuisine.getCuisineTypeList()));

            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(
                    dest => dest.rating,
                    opt => opt.MapFrom(src => src.rating.getRatingEntity()))
                .ForMember(
                    dest => dest.restaurantCuisine,
                    opt => opt.MapFrom(src => src.cuisineType.getRestaurantCuisineEntityAsList()));
        }
    }
}
