using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HydraApi.DTOs.Auth;
using HydraApi.DTOs.Roles;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.IdentityModel.Tokens;

namespace HydraApi.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public AuthResponseDTO CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        };

        foreach (var item in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item.Name));
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value ?? throw new NullReferenceException("")));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        var getUser = _userRepository.Get(user.Username);
        var role = getUser.Roles.Select(r => new RoleResponseDTO
        {
            Id = r.Id,
            Name = r.Name,
        }).ToList();

        return new AuthResponseDTO()
        {
            Token = serializedToken,
            Username = user.Username,
            Roles = role
        };
    }

    public AuthResponseDTO GetToken(AuthRequestDTO request)
    {
        var model = _userRepository.Get(request.Username);
        bool isCorrectUsername = request.Username == model.Username;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, model.Password);

        if (isCorrectPassword && isCorrectUsername)
        {
            return CreateToken(model);
        }

        throw new NullReferenceException("Username or Password is incorrect!");
    }
}