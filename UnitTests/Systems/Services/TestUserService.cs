using API.Config;
using API.Models;
using API.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using UnitTests.Fixtures;
using UnitTests.Helper;

namespace UnitTests.Systems.Services;

public class TestUserService
{
    [Fact]
    public async Task GetAllUser_WhenCalled_InvokesHttpGetRequest()
    {
        // Arrange
        var expectedResponse = UserFixtures.GetUsersTest();
        var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        const string endpoint = "https://example.com/users";
        
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var sut = new UserService(httpClient, config);
        
        // Act
        await sut.GetAllUser();

        // Assert
        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
        );
    }
    
    [Fact]
    public async Task GetAllUser_WhenHits404_ReturnEmptyListOfUser()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        const string endpoint = "https://example.com/users";
        
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var sut = new UserService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUser();

        // Assert
        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUser_WhenCalled_ReturnListOfUserOfExpectedSize()
    {
        // Arrange
        var expectedResponse = UserFixtures.GetUsersTest();
        var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        const string endpoint = "https://example.com/users";
        
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var sut = new UserService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUser();

        // Assert
        result.Count.Should().Be(expectedResponse.Count);
    }
    
    [Fact]
    public async Task GetAllUser_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange
        var expectedResponse = UserFixtures.GetUsersTest();
        const string endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);
        
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var sut = new UserService(httpClient, config);
        
        // Act
        await sut.GetAllUser();
        var uri = new Uri(endpoint);

        // Assert
        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => 
                req.Method == HttpMethod.Get && req.RequestUri == uri),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}