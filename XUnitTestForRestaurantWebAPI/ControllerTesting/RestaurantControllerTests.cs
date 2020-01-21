using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantData;
using RestaurantModel;
using RestaurantWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestForRestaurantWebAPI.ControllerTesting
{
    public class RestaurantControllerTests
    {
        private Mock<IRestaurantRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RestaurantController _controller;

        public RestaurantControllerTests()
        {
            _mockRepo = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new RestaurantController(_mockRepo.Object, _mockMapper.Object);
        }




        [Fact]
        public async Task GetAllRestaurants_ReturnsOk()
        {
            //act
            var result = await _controller.GetAllRestaurants();

            //assert
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }

        //[Fact]
        //public async Task GetAllRestaurants_CountExactNumberOfRestaurants()
        //{
        //    var x = new List<Restaurant>() { new Restaurant(), new Restaurant() };
        //    //Arrange
        //    _mockRepo.Setup(repo => repo.GetAllRestaurants())
        //        .Returns(Task.FromResult(new List<Restaurant>()));

        //    //Act
        //    var result = await _controller.GetAllRestaurants();

        //    //Assert
        //    Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        //}



        [Fact]
        public async Task GetRestaurantByID_ReturnsOk()
        {
            var restaurantDto = new RestaurantDto() { ID = 4 };
            //Arrange
            var result = await _controller.AddRestaurant(restaurantDto);

            //Act
            var res = await _controller.GetRestaurantByID(4);
            
            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(res.Result);
            //Assert.Equal(restaurant.ID, result.Value.ID);
        }

        [Fact]
        public async Task GetRestaurantByID_ReturnsNotFound()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetRestaurantByID(4))
                .Returns(Task.FromResult(new Restaurant() { ID = 4 }));

            //Act
            var result = await _controller.GetRestaurantByID(500);

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

       
    }
}
