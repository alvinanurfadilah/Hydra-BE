using System;
using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ITrainerSkillDetailRepository
{
    TrainerSkillDetail Get(int trainerId, string skillId);
    void Insert(TrainerSkillDetail model);
    void Delete(TrainerSkillDetail model);
}
