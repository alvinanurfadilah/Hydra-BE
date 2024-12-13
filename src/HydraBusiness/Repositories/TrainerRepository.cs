using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraBusiness.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly HydraContext _context;

    public TrainerRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Trainer> Get()
    {
        return _context.Trainers.ToList();
    }

    public List<Trainer> Get(string skillId)
    {
        return _context.Trainers
        .Include(t => t.TrainerSkillDetails)
        .Where(s => s.TrainerSkillDetails.Any(tsd => tsd.SkillId == skillId))
        .ToList();
    }

    public List<Trainer> Get(int pageNumber, int pageSize, string fullName)
    {
        return _context.Trainers
        .Where(t => (t.FirstName + t.LastName).ToLower().Contains(fullName ?? "".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Trainer Get(int id)
    {
        return _context.Trainers.Find(id);
    }

    public void Insert(Trainer trainer)
    {

        _context.Trainers.Add(trainer);
        _context.SaveChanges();
    }

    public void Update(Trainer trainer)
    {
        _context.Trainers.Update(trainer);
        _context.SaveChanges();
    }

    public void Delete(Trainer trainer)
    {
        _context.Trainers.Remove(trainer);
        _context.SaveChanges();
    }

    public int Count(string fullName)
    {
        return _context.Trainers
        .Where(t => (t.FirstName.ToLower() + t.LastName.ToLower()).Contains(fullName ?? "".ToLower()))
        .Count();
    }
}
