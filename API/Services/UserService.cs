using System.Net;
using API.Config;
using API.Models;
using Microsoft.Extensions.Options;

namespace API.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly UsersApiOptions _apiOptions;

    public UserService(HttpClient httpClient, IOptions<UsersApiOptions> apiOptions)
    {
        _httpClient = httpClient;
        _apiOptions = apiOptions.Value;
    }

    public async Task<List<User>> GetAllUser()
    {
        var usersResponse = await _httpClient.GetAsync(_apiOptions.Endpoint);

        if (usersResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<User> ();
        }

        var responseContent = usersResponse.Content;
        var allUser = await responseContent.ReadFromJsonAsync<List<User>>();

        return allUser.ToList();
    }
}