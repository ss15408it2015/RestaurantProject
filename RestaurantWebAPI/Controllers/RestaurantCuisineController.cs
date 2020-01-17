using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantData;
using RestaurantModel;
using RestaurantWebAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantID}/restaurantCuisineTypes")]
    public class RestaurantCuisineController : ControllerBase
    {
        private IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantCuisineController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository ??
               throw new ArgumentNullException(nameof(restaurantRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }



        /// <summary>
        /// it returns list of all the cuisineTypes of particular restaurant and takes restaurantID as arguments.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CuisineTypeDto>>> GetAllCuisineTypesOfRestaurant(int restaurantID)
        {
            if (! await _restaurantRepository.RestaurantExist(restaurantID))
                return NotFound();

            var cuisineTypeEntity = await _restaurantRepository.GetCuisineTypeOfRestaurant(restaurantID);

            return Ok(_mapper.Map<List<CuisineTypeDto>>(cuisineTypeEntity));
        }

        /// <summary>
        /// it add single cuisineType in perticular restaurant and takes restaurantID and cuisineTypeDto as arguments.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="cuisineTypeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CuisineTypeDto>> AddCuisineTypeToRestaurant(int restaurantID, CuisineTypeDto cuisineTypeDto)
        {
            if (!await _restaurantRepository.RestaurantExist(restaurantID))
                return NotFound();

            if (!await _restaurantRepository.CuisineTypeExist(cuisineTypeDto.cuisineID))
                return NotFound();

            RestaurantCuisine resCuisine = new RestaurantCuisine();
            resCuisine.cuisineTypeID = cuisineTypeDto.cuisineID;
            resCuisine.restaurantID = restaurantID;

            var resCuisineFromRepo = await _restaurantRepository.AddCuisineTypeToRestaurant(resCuisine);
            await _restaurantRepository.SaveAsync();

            cuisineTypeDto.cuisineType = resCuisineFromRepo.cuisineType.cuisineType;

            return Ok(cuisineTypeDto);
        }

        /// <summary>
        /// it removes cuisineType from particular restaurant and takes restaurantID and cuisineID as argument.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="cuisineID"></param>
        /// <returns></returns>
        [HttpDelete("{cuisineID}")]
        public async Task<ActionResult> RemoveCuisineTypeFromRestaurant(int restaurantID, int cuisineID)
        {
            _restaurantRepository.RemoveCuisineTypeFromRestaurant(restaurantID, cuisineID);
            if (await _restaurantRepository.SaveAsync() >= 1)
                return Ok();
            else
                return NotFound();
        }
    }
}
