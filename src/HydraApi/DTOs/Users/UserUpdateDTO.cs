using System;

namespace HydraApi.DTOs.Users;

public class UserUpdateDTO
{
    public string? Username { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<int> Roles { get; set; }
}
