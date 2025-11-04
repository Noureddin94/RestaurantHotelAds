using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using RestaurantHotelAds.API.Controllers;
using RestaurantHotelAds.Application.DTOs.AuthDtos;
using RestaurantHotelAds.Application.Services.AuthServices;
using RestaurantHotelAds.Core.Enums;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace RestaurantHotelAds.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "Test@123",
                FullName = "Test User",
                Role = UserRole.HotelOwner
            };

            var authResponse = new AuthResponseDto
            {
                Token = "jwt_token_here",
                Email = "test@example.com",
                UserId = Guid.NewGuid().ToString()
            };

            _mockAuthService.Setup(service => service.RegisterAsync(registerDto))
                .ReturnsAsync(authResponse);

            // Act
            var result = await _authController.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AuthResponseDto>(okResult.Value);
            Assert.Equal(authResponse.Token, returnValue.Token);
            Assert.Equal(authResponse.Email, returnValue.Email);
        }

        [Fact]
        public async Task Register_WithExistingEmail_ReturnsBadRequest()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "existing@example.com",
                Password = "Test@123",
                FullName = "Test User",
                Role = UserRole.HotelOwner
            };

            _mockAuthService.Setup(service => service.RegisterAsync(registerDto))
                .ThrowsAsync(new InvalidOperationException("Email already registered."));

            // Act
            var result = await _authController.Register(registerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            // FIXED: Use JsonSerializer to handle anonymous types
            var json = JsonSerializer.Serialize(badRequestResult.Value);
            var responseObject = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            Assert.NotNull(responseObject);
            Assert.True(responseObject.ContainsKey("message"));
            Assert.Equal("Email already registered.", responseObject["message"]?.ToString());
        }
    }
}
