using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface IRoleRepository
{
    List<Role> Get();
    Role GetById(int id);
    List<Role> GetByUsername(string username);
}