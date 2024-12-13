using HydraApi.DTOs.Roles;
using HydraDataAccess.Models;

namespace HydraApi.DTOs.Users;

public class UserResponseDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<RoleResponseDTO> Roles { get; set; }
}