using System;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraBusiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly HydraContext _context;

    public CategoryRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Category> Get()
    {
        return _context.Categories.ToList();
    }

    public List<Category> Get(int pageNumber, int pageSize, string name, string level)
    {
        return _context.Categories
        .Where(cat => cat.Name.ToLower().Contains(name ?? "".ToLower()) && cat.Level.ToLower().Contains(level ?? "".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Category Get(int id)
    {
        return _context.Categories.Find(id) ?? throw new NullReferenceException();
    }

    public void Insert(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    public int Count(string name, string level)
    {
        return _context.Categories
        .Where(cat => cat.Name.ToLower().Contains(name ?? "".ToLower()) && cat.Level.ToLower().Contains(level ?? "".ToLower())).Count();
    }
}
