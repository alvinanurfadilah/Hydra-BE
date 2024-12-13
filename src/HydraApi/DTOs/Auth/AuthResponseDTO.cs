using HydraApi.DTOs.Roles;
using HydraDataAccess.Models;

namespace HydraApi.DTOs.Auth;

public class AuthResponseDTO
{
    public string Token { get; set; }
    public string Username { get; set; }
    public List<RoleResponseDTO> Roles { get; set; }
}