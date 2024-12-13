using System.Security.Claims;
using HydraApi.DTOs.Auth;
using HydraApi.DTOs.Roles;
using HydraApi.Services;
using HydraDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    private readonly UserService _userService;

    public AuthController(AuthService service, UserService userService)
    {
        _service = service;
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<User?> Get()
    {
        try
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var user = _userService.Get(username);
            var role = user.Roles.Select(r => new RoleResponseDTO
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();

            var requestToken = new AuthResponseDTO()
            {
                Token = _service.CreateToken(user).Token,
                Username = username,
                Roles = role
            };

            return Ok(requestToken);
        }
        catch (System.Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpPost]
    public ActionResult<string> Login(AuthRequestDTO dto)
    {
        try
        {
            var response = _service.GetToken(dto);
            return Ok(response);
        }
        catch (System.Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}