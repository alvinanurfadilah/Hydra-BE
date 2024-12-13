using System;
using HydraBusiness.Interfaces;

namespace HydraApi.Services;

public class TrainerSkillDetailService
{
    private readonly ITrainerSkillDetailRepository _repository;

    public TrainerSkillDetailService(ITrainerSkillDetailRepository repository)
    {
        _repository = repository;
    }

    public void Delete(int trainerId, string skillId)
    {
        var model = _repository.Get(trainerId, skillId);
        _repository.Delete(model);
    }
}
