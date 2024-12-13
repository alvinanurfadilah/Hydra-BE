using System;
using HydraApi.DTOs.Trainers;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class TrainerService
{
    private readonly ITrainerRepository _repository;
    private readonly ITrainerSkillDetailRepository _trainerSkillDetailRepository;

    public TrainerService(ITrainerRepository repository, ITrainerSkillDetailRepository trainerSkillDetailRepository)
    {
        _repository = repository;
        _trainerSkillDetailRepository = trainerSkillDetailRepository;
    }

    public bool ConvertToBool(int? variabel)
    {
        bool isTrue;
        if (variabel == 1)
        {
            isTrue = true;
        } else
        {
            isTrue = false;
        }

        return isTrue;
    }

    public List<TrainerResponseDTO> Get(string skillId)
    {
        return _repository.Get(skillId).Select(trainer => new TrainerResponseDTO()
        {
            Id = trainer.Id,
            Username = trainer.Username,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            BirthDate = trainer.BirthDate,
            Gender = trainer.Gender,
            PhoneNumber = trainer.PhoneNumber,
            IsAvailable = trainer.IsAvailable,
            IsActive = trainer.IsActive
        }).ToList();
    }

    public List<TrainerResponseDTO> Get(int pageNumber, int pageSize, string fullName)
    {
        return _repository.Get(pageNumber, pageSize, fullName).Select(trainer => new TrainerResponseDTO()
        {
            Id = trainer.Id,
            Username = trainer.Username,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            BirthDate = trainer.BirthDate,
            Gender = trainer.Gender,
            PhoneNumber = trainer.PhoneNumber,
            IsAvailable = trainer.IsAvailable,
            IsActive = trainer.IsActive
        }).ToList();
    }

    public Trainer Get(int id)
    {
        return _repository.Get(id);
    }

    public void Insert(TrainerInsertDTO dto)
    {
        var trainer = new Trainer()
        {
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            IsAvailable = ConvertToBool(dto.IsAvailable),
            IsActive = ConvertToBool(dto.IsActive)
        };

        _repository.Insert(trainer);

        // var lastTrainerId = trainer.Id;
        // foreach (var item in dto.Skills)
        // {
        //     var trainerSkill = new TrainerSkillDetail()
        //     {
        //         TrainerId = lastTrainerId,
        //         SkillId = item,
        //         AssignedDate = DateTime.Now,
        //         AchievedDate = DateTime.Now
        //     };

        //     _trainerSkillDetailRepository.Insert(trainerSkill);
        // }
    }

    public void Update(TrainerUpdateDTO dto)
    {
        var model = _repository.Get(dto.Id);
        model.Username = dto.Username;
        model.FirstName = dto.FirstName;
        model.LastName = dto.LastName;
        model.BirthDate = dto.BirthDate;
        model.Gender = dto.Gender;
        model.PhoneNumber = dto.PhoneNumber;
        model.IsAvailable = ConvertToBool(dto.IsAvailable);
        model.IsActive = ConvertToBool(dto.IsActive);

        _repository.Update(model);
    }

    public void Delete(int id)
    {
        var model = _repository.Get(id);
        _repository.Delete(model);
    }

    public int Count(string fullName)
    {
        return _repository.Count(fullName);
    }
}
