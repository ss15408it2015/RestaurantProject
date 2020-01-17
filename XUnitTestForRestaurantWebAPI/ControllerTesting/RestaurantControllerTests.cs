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
        public void GetAllRestaurants_ReturnsOk()
        {
            var result = _controller.GetAllRestaurants();
            Assert.IsType<Task<ActionResult<List<RestaurantDto>>>>(result);
        }

        [Fact]
        public void GetAllRestaurants_CountExactNumberOfRestaurants()
        {
            //_mockRepo.Setup(repo => repo.GetAllRestaurants())
            //    .Returns((Func<Task<ActionResult<List<RestaurantDto>>>>)(async () =>
            //    {
            //        await {new RestaurantDto(), new RestaurantDto()};
            //    }))();

            var result = _controller.GetAllRestaurants();
            Assert.Equal(22,result.Result.Value.Count);
        }
    }
}
