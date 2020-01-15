using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantData;
using RestaurantModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("api/cuisineTypes")]
    public class CuisineController :ControllerBase
    {
        public IRestaurantRepository _restaurantRepository;
        public IMapper _mapper;

        public CuisineController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository??
                throw new ArgumentNullException(nameof(restaurantRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        



        [HttpGet]
        public async Task<ActionResult<List<CuisineTypeDto>>> GetAllCuisineTypes()
        {
            var cuisineTypes = await _restaurantRepository.GetAllCuisineTypes();

            if (cuisineTypes != null)
                return Ok(_mapper.Map<List<CuisineTypeDto>>(cuisineTypes));
            else
                return NotFound();
        }


        [HttpGet("{cuisineTypeID}", Name = "GetSingleCuisineType")]
        public async Task<ActionResult<CuisineTypeDto>> GetSingleCuisineType(int cuisineTypeID)
        {
            var cuisineType = await _restaurantRepository.GetSingleCuisineType(cuisineTypeID);
            if (cuisineType != null)
                return Ok(_mapper.Map<CuisineTypeDto>(cuisineType));
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> AddCuisineType(CuisineTypeDto cuisineTypeDto)
        {
            var cuisineTypeFromRepo = await _restaurantRepository.AddCuisineType(_mapper.Map<CuisineType>(cuisineTypeDto));
            await _restaurantRepository.SaveAsync();

            var cuisineTypeToReturn = _mapper.Map<CuisineTypeDto>(cuisineTypeFromRepo);

            return CreatedAtRoute("GetSingleCuisineType",
                new { cuisineTypeID = cuisineTypeToReturn.cuisineID },
                cuisineTypeToReturn);
        }


        [HttpDelete("{cuisineTypeID}")]
        public async Task<ActionResult> RemoveSingleCuisineType(int cuisineTypeID)
        {
            _restaurantRepository.RemoveSingleCuisineType(cuisineTypeID);
            if (await _restaurantRepository.SaveAsync() >= 1)
                return Ok();
            else
                return NotFound();
        }


        [HttpPut("{cuisineTypeID}")]
        public async Task<ActionResult> UpdateCuisineType(int cuisineTypeID, CuisineTypeDto cuisineTypeDto)
        {
            if (!await _restaurantRepository.CuisineTypeExist(cuisineTypeID))
                return NotFound();


            var cuisineTypeEntity = _mapper.Map<CuisineType>(cuisineTypeDto);
            cuisineTypeEntity.ID = cuisineTypeID;

            _restaurantRepository.UpdateCuisineType(cuisineTypeEntity);
            await _restaurantRepository.SaveAsync();

            var cuisineTypeToReturn = _mapper.Map<CuisineTypeDto>(cuisineTypeEntity);

            return CreatedAtRoute("GetSingleCuisineType",
                new { cuisineTypeID = cuisineTypeToReturn.cuisineID },
                cuisineTypeToReturn);
        }
    }
}
