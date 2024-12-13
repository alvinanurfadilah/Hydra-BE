using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraBusiness.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HydraContext _context;

    public UserRepository(HydraContext context)
    {
        _context = context;
    }

    public List<User> Get()
    {
        return _context.Users.ToList();
    }

    public User Get(string username)
    {
        return _context.Users
        .Include(u => u.Roles)
        .FirstOrDefault(u => u.Username == username) ?? throw new NullReferenceException("Username or Password is incorrect!");
    }

    public List<User> Get(int pageNumber, int pageSize, string username)
    {
        return _context.Users.Where(u => u.Username.ToLower().Contains(username ?? "".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public void Insert(User user)
    {
        try
        {
            // List<Role> roles = new List<Role>();
            // roles.Add(new Role() {Id = 1, Name = "Admin"});
            // user.Roles = roles;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Update(User user)
    {
        try
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Delete(User user)
    {
        try
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public int Count(string username)
    {
        return _context.Users.Where(u => u.Username.ToLower().Contains(username ?? "")).Count();
    }
}