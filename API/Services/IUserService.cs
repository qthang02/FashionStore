using API.Models;

namespace API.Services;

public interface IUserService
{
    Task<List<User>> GetAllUser();
}