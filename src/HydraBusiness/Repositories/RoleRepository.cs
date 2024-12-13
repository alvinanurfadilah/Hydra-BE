using System.Data.Common;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraBusiness.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly HydraContext _context;

    public RoleRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Role> Get()
    {
        return _context.Roles.ToList();
    }

    public Role GetById(int id)
    {
        return _context.Roles.Find(id);
    }

    public List<Role> GetByUsername(string username)
    {
        throw new Exception();
    }
}