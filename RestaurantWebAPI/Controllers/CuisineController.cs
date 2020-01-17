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





        /// <summary>
        /// It provides all the cuisineTypes.
        /// </summary>
        /// <returns>return ActionResult of type List of CuisineTypeDto</returns>
        [HttpGet]
        public async Task<ActionResult<List<CuisineTypeDto>>> GetAllCuisineTypes()
        {
            var cuisineTypes = await _restaurantRepository.GetAllCuisineTypes();

            if (cuisineTypes != null)
                return Ok(_mapper.Map<List<CuisineTypeDto>>(cuisineTypes));
            else
                return NotFound();
        }


        /// <summary>
        /// It returns a single cuisineType.
        /// </summary>
        /// <param name="cuisineTypeID">id of the cuisineType you want to get</param>
        /// <returns>return ActionResult of type CuisineTypeDto</returns>
        [HttpGet("{cuisineTypeID}", Name = "GetSingleCuisineType")]
        public async Task<ActionResult<CuisineTypeDto>> GetSingleCuisineType(int cuisineTypeID)
        {
            var cuisineType = await _restaurantRepository.GetSingleCuisineType(cuisineTypeID);
            if (cuisineType != null)
                return Ok(_mapper.Map<CuisineTypeDto>(cuisineType));
            else
                return NotFound();
        }


        /// <summary>
        /// To Add a new CuisineType.
        /// </summary>
        /// <param name="cuisineTypeDto">CuisineTypeDto </param>
        /// <returns>It shows the ActionResult of added CuisineType</returns>
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


        /// <summary>
        /// To Remove a CuisineType.
        /// </summary>
        /// <param name="cuisineTypeID">id of the cuisineType you want to delete</param>
        /// <returns></returns>
        [HttpDelete("{cuisineTypeID}")]
        public async Task<ActionResult> RemoveSingleCuisineType(int cuisineTypeID)
        {
            _restaurantRepository.RemoveSingleCuisineType(cuisineTypeID);
            if (await _restaurantRepository.SaveAsync() >= 1)
                return Ok();
            else
                return NotFound();
        }


        /// <summary>
        /// To Update an existing CuisineType.
        /// </summary>
        /// <param name="cuisineTypeDto">CuisineTypeDto</param>
        /// <returns>It shows the ActionResult of updated CuisineType</returns>
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
