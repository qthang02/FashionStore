using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name="GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUser();

        if (users.Any())
        {
            return Ok(users);
        }

        return NotFound();
    }
}