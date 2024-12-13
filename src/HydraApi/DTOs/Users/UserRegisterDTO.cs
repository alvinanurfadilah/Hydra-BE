using HydraApi.DTOs.Roles;
using HydraDataAccess.Models;

namespace HydraApi.DTOs.Users;

public class UserRegisterDTO
{
    public string? Username { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<int> Roles { get; set; }
}