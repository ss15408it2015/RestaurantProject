using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantData;
using RestaurantModel;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository ??
                throw new ArgumentNullException(nameof(restaurantRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }



        [HttpGet()]
        public async Task<ActionResult<List<RestaurantDto>>> GetALLRestaurants()
        {
            var restaurantFromRepo = await _restaurantRepository.GetAllRestaurants();
            
            if (restaurantFromRepo != null)
                return Ok(_mapper.Map<List<RestaurantDto>>(restaurantFromRepo));
            else
                return NotFound();
        }

        [HttpGet("{restaurantID}", Name = "GetRestaurant")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurantByID(int restaurantID)
        {
            var restaurantFromRepo = await _restaurantRepository.GetRestaurantByID(restaurantID);
            if (restaurantFromRepo != null)
                return Ok(_mapper.Map<RestaurantDto>(restaurantFromRepo));
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> AddRestaurant(RestaurantDto restaurantDto)
        {
            var restaurantEntity = _mapper.Map<Restaurant>(restaurantDto);
            
            var restaurantFromRepo = _restaurantRepository.AddRestaurant(restaurantEntity);
            await _restaurantRepository.SaveAsync();

            var restaurantToReturn = _mapper.Map<RestaurantDto>(restaurantFromRepo);
            return CreatedAtRoute("GetRestaurant",
                new { restaurantID = restaurantToReturn.ID },
                restaurantToReturn);
        }

        [HttpPut("{restaurantID}", Name = "Update")]
        public async Task<ActionResult<RestaurantDto>> UpdateRestaurant(int restaurantID, RestaurantDto resDto)
        {
            if (! await _restaurantRepository.RestaurantExist(restaurantID))
                return NotFound();

            var resEntity = _mapper.Map<Restaurant>(resDto);
            resEntity.ID = restaurantID;
            var resUpdatedEntity = _restaurantRepository.UpdateRestaurant(resEntity);
            await _restaurantRepository.SaveAsync();

            var restaurantToReturn = _mapper.Map<RestaurantDto>(resUpdatedEntity);
            return CreatedAtRoute("GetRestaurant",
                new { restaurantID = restaurantToReturn.ID },
                restaurantToReturn);
        }

        [HttpPatch("{restaurantID}")]
        public async Task<ActionResult<RestaurantDto>> PartialUpdateRestaurant(int restaurantID, JsonPatchDocument<RestaurantDto> restaurantPatchData)
        {
            var resToPatch = _mapper.Map<RestaurantDto>(_restaurantRepository.GetRestaurantByID(restaurantID));

            if (restaurantPatchData == null || resToPatch == null)
                return NotFound();

            restaurantPatchData.ApplyTo(resToPatch);

            var resEntity = _mapper.Map<Restaurant>(resToPatch);
            resEntity.ID = restaurantID;
            var resUpdatedEntity = _restaurantRepository.UpdateRestaurant(resEntity);
            var restaurantToReturn = _mapper.Map<RestaurantDto>(resUpdatedEntity);
            await _restaurantRepository.SaveAsync();

            return CreatedAtRoute("GetRestaurant",
                new { restaurantID = restaurantToReturn.ID },
                restaurantToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRestaurant(int ID)
        {
            _restaurantRepository.RemoveRestaurant(ID);
            if (await _restaurantRepository.SaveAsync() >= 1)
                return Ok();
            else
                return NotFound();
        }
    }
}