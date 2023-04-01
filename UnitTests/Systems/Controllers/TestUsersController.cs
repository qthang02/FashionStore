using API.Controllers;
using API.Models;
using API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200() 
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUser())
            .ReturnsAsync(new List<User>(){
                new()
                {
                    Id = 1,
                    Name = "Test User 1",
                    Email = "example@email.com",
                    Address = new Address()
                    {
                        Street = "Test Street 1",
                        City = "Test City 1",
                        ZipCode = "12345"
                    }
                }
            });

        var sul = new UserController(mockUserService.Object);
        
        // Act
        var result = (OkObjectResult)await sul.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact] 
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUser())
            .ReturnsAsync(new List<User>());
        
        var sul = new UserController(mockUserService.Object);
        
        // Act
        await sul.Get();
        
        // Assert
        mockUserService.Verify(service => service.GetAllUser(), Times.Once);
    }
    
    [Fact]
    public async Task Get_OnSuccess_ReturnsAllUsers()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(service => service.GetAllUser())
            .ReturnsAsync(new List<User>(){
                new()
                {
                    Id = 1,
                    Name = "Test User 1",
                    Email = "example@email.com",
                    Address = new Address()
                    {
                        Street = "Test Street 1",
                        City = "Test City 1",
                        ZipCode = "12345"
                    }
                }
            });
        
        var sul = new UserController(mockUserService.Object);
        
        //Act
        var result = (OkObjectResult)await sul.Get();
        
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }
    
    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        //Arrange
        var mockUserService = new Mock<IUserService>();

        mockUserService
            .Setup(service => service.GetAllUser())
            .ReturnsAsync(new List<User>());
        
        var sul = new UserController(mockUserService.Object);
        
        //Act
        var result = await sul.Get();
        
        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}