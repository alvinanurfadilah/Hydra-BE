using HydraApi.DTOs.Roles;
using HydraApi.DTOs.Users;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository repository, IRoleRepository roleRepository)
    {
        _repository = repository;
        _roleRepository = roleRepository;
    }

    public List<UserResponseDTO> Get()
    {
        return _repository.Get().Select(u => new UserResponseDTO()
        {
            Username = u.Username,
            Email = u.Email
        }).ToList();
    }

    public List<UserResponseDTO> Get(int pageNumber, int pageSize, string username)
    {
        return _repository.Get(pageNumber, pageSize, username).Select(user => new UserResponseDTO()
        {
            Username = user.Username,
            Email = user.Email,
        }).ToList();
    }

    public User Get(string username)
    {
        var user = _repository.Get(username);
        var role = user.Roles.Select(r => new Role
        {
            Id = r.Id,
            Name = r.Name
        }
        ).ToList();

        return new User()
        {
            Username = user.Username,
            Email = user.Email, 
            Password = user.Password,
            Roles = role
        };
    }

    public void Insert(UserRegisterDTO dto)
    {
        List<Role> roles = new List<Role>();

        foreach (var item in dto.Roles)
        {
            var getRole = _roleRepository.GetById(item);
            roles.Add(getRole);
        }

        var user = new User()
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Roles = roles
        };

        _repository.Insert(user);
    }

    public void Update(UserUpdateDTO dto)
    {
        List<Role> roles = new List<Role>();

        foreach (var item in dto.Roles)
        {
            var getRole = _roleRepository.GetById(item);
            roles.Add(getRole);
        }

        var model = _repository.Get(dto.Username);
        model.Email = dto.Email;
        model.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        model.Roles = roles;

        _repository.Update(model);
    }

    public void Delete(string username)
    {
        var model = _repository.Get(username);
        _repository.Delete(model);
    }

    public int Count(string username)
    {
        return _repository.Count(username);
    }
}