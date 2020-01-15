using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantModel.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingDto, Rating>();
        }
    }
}
