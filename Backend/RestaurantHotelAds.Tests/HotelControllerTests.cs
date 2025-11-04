using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantHotelAds.API.Controllers;
using RestaurantHotelAds.Application.DTOs.HotelDtos;
using RestaurantHotelAds.Application.Services.HotelsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Tests
{
    public class HotelControllerTests
    {
        private readonly Mock<IHotelService> _mockHotelService;
        private readonly HotelController _hotelController;

        public HotelControllerTests()
        {
            _mockHotelService = new Mock<IHotelService>();
            _hotelController = new HotelController(_mockHotelService.Object);
        }

        private void SetupAuthenticatedUser(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, "user@example.com")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            _hotelController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Fact]
        public async Task GetAllHotels_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Arrange - UNAUTHENTICATED user
            var identity = new ClaimsIdentity(); // No authentication type = not authenticated
            var principal = new ClaimsPrincipal(identity);

            _hotelController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            // Act
            var result = await _hotelController.GetAllHotels();

            // Assert - FIXED: Handle the wrapped unauthorized response
            var actionResult = Assert.IsType<ActionResult<List<HotelDto>>>(result);
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);

            // Check the unauthorized response message
            var responseJson = JsonSerializer.Serialize(unauthorizedResult.Value);
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            Assert.NotNull(responseObject);
            Assert.Equal("User is not authenticated", responseObject["message"]?.ToString());
        }

        [Fact]
        public async Task GetHotelById_WithValidId_ReturnsHotel()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var hotelId = Guid.NewGuid();
            SetupAuthenticatedUser(userId);

            var hotel = new HotelDto
            {
                Id = hotelId,
                Name = "Test Hotel",
                Address = "Test Address",
                TotalRooms = 15,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoomsCount = 8,
                PendingAdsCount = 3
            };

            _mockHotelService.Setup(service => service.GetHotelByIdAsync(hotelId, userId))
                .ReturnsAsync(hotel);

            // Act
            var result = await _hotelController.GetHotelById(hotelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            Assert.NotNull(responseObject);
            Assert.Equal("Hotel retrieved successfully", responseObject["message"]?.ToString());

            // Extract the hotel data
            var dataJson = responseObject["data"]?.ToString();
            var returnedHotel = JsonSerializer.Deserialize<HotelDto>(dataJson!);

            Assert.NotNull(returnedHotel);
            Assert.Equal(hotelId, returnedHotel.Id);
            Assert.Equal("Test Hotel", returnedHotel.Name);
        }

        [Fact]
        public async Task GetHotelById_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var hotelId = Guid.NewGuid();
            SetupAuthenticatedUser(userId);

            _mockHotelService.Setup(service => service.GetHotelByIdAsync(hotelId, userId))
                .ReturnsAsync((HotelDto?)null);

            // Act
            var result = await _hotelController.GetHotelById(hotelId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            var responseJson = JsonSerializer.Serialize(notFoundResult.Value);
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(responseJson);

            Assert.NotNull(responseObject);
            Assert.Equal("Hotel not found or you don't have access to it", responseObject["message"]?.ToString());
        }
    }
}
