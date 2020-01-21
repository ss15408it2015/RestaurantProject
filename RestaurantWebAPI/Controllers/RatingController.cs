using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    [Route("api/restaurants/{restaurantID}/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RatingController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository??
                throw new ArgumentNullException(nameof(restaurantRepository));
            _mapper = mapper??
                throw new ArgumentNullException(nameof(mapper));
        }



        /// <summary>
        /// it returns single rating of restaurant By using restaurantID and ratingID.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="ratingID"></param>
        /// <returns></returns>
        [HttpGet("{ratingID}", Name = "GetSingleRating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RatingDto>> GetSingleRating(int restaurantID, int ratingID)
        {
            var ratingEntity = await _restaurantRepository.GetSingleRating(restaurantID, ratingID);

            if (ratingEntity != null)
                return Ok(_mapper.Map<RatingDto>(ratingEntity));
            else
                return NotFound();
        }


        /// <summary>
        /// it returns list of all ratings of perticular restaurant By using restaurantID.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RatingDto>>> GetAllRatings(int restaurantID)
        {
            var ratingEntity = await _restaurantRepository.GetAllRatings(restaurantID);

            if (ratingEntity != null)
                return Ok(_mapper.Map<List<RatingDto>>(ratingEntity));
            else
                return NotFound();
        }


        /// <summary>
        /// it add single rating in perticular restaurant By using restaurantID and ratingData in RatingDto form.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="ratingDtoForAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddSingleRating(int restaurantID, RatingDtoForAdd ratingDtoForAdd)
        {
            if (await _restaurantRepository.RestaurantExist(restaurantID))
            {
                var ratingEntity = _mapper.Map<Rating>(ratingDtoForAdd);
                
                var ratingEntityFromRepo = await _restaurantRepository.AddSingleRating(restaurantID, ratingEntity);
                await _restaurantRepository.SaveAsync();

                var ratingToReturn = _mapper.Map<RatingDto>(ratingEntityFromRepo);
                return CreatedAtRoute("GetSingleRating",
                    new 
                    { 
                        ratingID = ratingToReturn.ID , 
                        restaurantID = restaurantID
                    }
                    , ratingToReturn);
            }
            else
                return NotFound();
        }


        /// <summary>
        /// it updates single rating of restaurant By using restaurantID, ratingID and ratingData in RatingDto form.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="ratingID"></param>
        /// <param name="ratingDtoForAdd"></param>
        /// <returns></returns>
        [HttpPut("{ratingID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateRating(int restaurantID, int ratingID, RatingDtoForAdd ratingDtoForAdd)
        {
            if (!await _restaurantRepository.RestaurantExist(restaurantID))
                return NotFound();
            

            var ratingEntity = _mapper.Map<Rating>(ratingDtoForAdd);

            _restaurantRepository.UpdateRating(restaurantID, ratingID, ratingEntity);
            await _restaurantRepository.SaveAsync();

            var ratingToReturn = _mapper.Map<RatingDto>(ratingEntity);

            return CreatedAtRoute("GetSingleRating",
                   new
                   {
                       ratingID = ratingToReturn.ID,
                       restaurantID = restaurantID
                   }
                    , ratingToReturn);
        }


        /// <summary>
        /// it removes single rating of restaurant By using restaurantID and ratingID.
        /// </summary>
        /// <param name="restaurantID"></param>
        /// <param name="ratingID"></param>
        /// <returns></returns>
        [HttpDelete("{ratingID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveSingleRating(int restaurantID, int ratingID)
        {
            _restaurantRepository.RemoveSingleRating(restaurantID, ratingID);
            if (await _restaurantRepository.SaveAsync() >= 1)
                return Ok();
            else
                return NotFound();
        }
    }
}
