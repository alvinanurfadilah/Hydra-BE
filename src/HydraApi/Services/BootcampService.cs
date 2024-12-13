using HydraApi.DTOs.Bootcamps;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class BootcampService
{
    private readonly IBootcampRepository _repository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICourseRepository _courseRepository;

    public BootcampService(IBootcampRepository repository, ICandidateRepository candidateRepository, ICourseRepository courseRepository)
    {
        _repository = repository;
        _candidateRepository = candidateRepository;
        _courseRepository = courseRepository;
    }

    public string GetProgress(int progress)
    {
        string progressName = "";
        if (progress == 0)
        {
            progressName = "Cancelled";
        }
        else if (progress == 1)
        {
            progressName = "Planned";
        }
        else if (progress == 2)
        {
            progressName = "Active";
        }
        else if (progress == 3)
        {
            progressName = "Completed";
        }

        return progressName;
    }

    public List<BootcampResponseDTO> Get()
    {
        return _repository.Get().Select(bc => new BootcampResponseDTO()
        {
            Id = bc.Id,
            Description = bc.Description,
            StartDate = bc.StartDate,
            EndDate = bc.EndDate
        }).ToList();
    }

    private string GetTrainerName(int bootcampId)
    {
        var trainerName = _courseRepository.GetCourseByBootcampId(bootcampId);
        if (trainerName != null)
        {
            return trainerName.TrainerSkillDetail.Trainer.FirstName + " " + trainerName.TrainerSkillDetail.Trainer.LastName;
        }
        else
        {
            return "Not Started";
        }
    }

    private string GetSkillName(int bootcampId)
    {
        var skillName = _courseRepository.GetCourseByBootcampId(bootcampId);
        if (skillName != null)
        {
            return skillName.TrainerSkillDetail.Skill.Name;
        }
        else
        {
            return "Not Started";
        }

    }

    public List<BootcampResponseDTO> Get(int pageNumber, int pageSize, int id)
    {
        return _repository.Get(pageNumber, pageSize, id)
        .Select(bc => new BootcampResponseDTO()
        {
            Id = bc.Id,
            Description = bc.Description,
            // CountCandidate = _candidateRepository.Count(bc.Id),
            StartDate = bc.StartDate,
            EndDate = bc.EndDate,
            // TrainerName = GetTrainerName(bc.Id),
            // SkillName = GetSkillName(bc.Id)
        }).ToList();
    }

    public BootcampClass GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Insert(BootcampInsertDTO dto)
    {
        var bc = new BootcampClass()
        {
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };

        _repository.Insert(bc);
    }

    public void Update(BootcampUpdateDTO dto)
    {
        var bc = _repository.GetById(dto.Id);
        bc.Description = dto.Description;
        bc.StartDate = dto.StartDate;
        bc.EndDate = dto.EndDate;

        _repository.Update(bc);
    }

    public int Count(int id)
    {
        return _repository.Count(id);
    }

    public void UpdatePlan(BootcampUpdatePlanDTO dto)
    {
        var bc = _repository.GetById(dto.Id);
        bc.Progress = dto.Progress;
        bc.EndDate = dto.EndDate;

        _repository.Update(bc);
    }

    public List<BootcampResponseSubPageDTO> GetSubPage(int pageNumber, int pageSize, int id, int progress)
    {
        return _repository.GetSubPage(pageNumber, pageSize, id, progress).Select(bc=> new BootcampResponseSubPageDTO() {
            Id = bc.Id,
            Description = bc.Description,
            StartDate = bc.StartDate,
            EndDate = bc.EndDate,
            TotalCandidate = _candidateRepository.Count(bc.Id),
            TrainerName = GetTrainerName(bc.Id),
            SkillName = GetSkillName(bc.Id)
            // TrainerName = bc.Courses.Select(c => c.TrainerSkillDetail.Trainer.FirstName + c.TrainerSkillDetail.Trainer.LastName).FirstOrDefault(),
            // SkillName = bc.Courses.Select(c => c.TrainerSkillDetail.Skill.Name).FirstOrDefault()
        }).ToList();
    }

    public int CountCandidate(int id, int progress)
    {
        return _repository.CountCandidate(id, progress);
    }
}
