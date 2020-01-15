using AutoMapper;
using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Profiles
{
    public class CuisineTypeProfile : Profile
    {
        public CuisineTypeProfile()
        {
            CreateMap<CuisineType, CuisineTypeDto>()
                .ForMember(
                    dest => dest.cuisineID,
                    opt => opt.MapFrom(src => src.ID));

            CreateMap<CuisineTypeDto, CuisineType>()
                 .ForMember(
                    dest => dest.ID,
                    opt => opt.MapFrom(src => src.cuisineID));
        }
    }
}
