using System;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraBusiness.Repositories;

public class TrainerSkillDetailRepository : ITrainerSkillDetailRepository
{
    private readonly HydraContext _context;

    public TrainerSkillDetailRepository(HydraContext context)
    {
        _context = context;
    }

    public TrainerSkillDetail Get(int trainerId, string skillId)
    {
        return _context.TrainerSkillDetails.Where(tsd => tsd.TrainerId == trainerId && tsd.SkillId == skillId).FirstOrDefault();
    }

    public void Insert(TrainerSkillDetail model)
    {
        _context.TrainerSkillDetails.Add(model);
        _context.SaveChanges();
    }

    public void Delete(TrainerSkillDetail model)
    {
        _context.TrainerSkillDetails.Remove(model);
        _context.SaveChanges();
    }
}
