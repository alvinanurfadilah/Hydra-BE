using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraBusiness.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly HydraContext _context;

    public SkillRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Skill> Get()
    {
        return _context.Skills.ToList(); 
    }

    public List<Skill> Get(int trainerId)
    {
        return _context.Skills
        .Include(s => s.TrainerSkillDetails)
        .Where(s => s.TrainerSkillDetails.Any(tsd => tsd.TrainerId == trainerId))
        .ToList();
    } 

    public List<Skill> Get(int pageNumber, int pageSize, int categoryId)
    {
        return _context.Skills
        .Include(skill => skill.Category)
        .Where(skill => skill.CategoryId == categoryId || 0 == categoryId)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Skill GetById(string id)
    {
        return _context.Skills.Find(id) ?? throw new NullReferenceException();
    }

    public void Insert(Skill skill)
    {
        _context.Skills.Add(skill);
        _context.SaveChanges();
    }

    public void Update(Skill skill)
    {
        _context.Skills.Update(skill);
        _context.SaveChanges();
    }

    public void Delete(Skill skill)
    {
        _context.Skills.Remove(skill);
        _context.SaveChanges();
    }

    public int Count(int categoryId)
    {
        return _context.Skills
        .Include(skill => skill.Category)
        .Where(skill => skill.CategoryId == categoryId || 0 == categoryId)
        .Count();
    }
}
