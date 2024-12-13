using System;
using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ICategoryRepository
{
    List<Category> Get();
    List<Category> Get(int pageNumber, int pageSize, string name, string level);
    Category Get(int id);
    void Insert(Category category);
    void Update(Category category);
    void Delete(Category category);

    int Count(string name, string level);
}
