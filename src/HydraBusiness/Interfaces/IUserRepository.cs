using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface IUserRepository
{
    List<User> Get();
    User Get(string username);
    List<User> Get(int pageNumber, int pageSize, string username);
    void Insert(User user);
    void Update(User user);
    void Delete(User user);

    int Count(string username);
}