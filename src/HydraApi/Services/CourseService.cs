using HydraApi.DTOs.Courses;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class CourseService
{
    private readonly ICourseRepository _repository;

    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
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

    public List<CourseResponseDTO> Get(int pageNumber, int pageSize, int bootcampId)
    {
        return _repository.Get(pageNumber, pageSize, bootcampId)
        .Select(c => new CourseResponseDTO()
        {
            Skill = c.TrainerSkillDetail.Skill.Name,
            Trainer = c.TrainerSkillDetail.Trainer.FirstName + " " + c.TrainerSkillDetail.Trainer.LastName,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
            Progress = c.Progress.ToString(),
            EvaluationDate = c.EvaluationDate
        }).ToList();
    }

    public List<CourseResponseDTO> GetByBootcampId(int bootcampId)
    {
        return _repository.Get(bootcampId)
        .Select(c => new CourseResponseDTO()
        {
            Id = c.Id,
            Skill = c.TrainerSkillDetail.Skill.Name,
            Trainer = c.TrainerSkillDetail.Trainer.FirstName + " " + c.TrainerSkillDetail.Trainer.LastName,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
            Progress = GetProgress(c.Progress),
            EvaluationDate = c.EvaluationDate
        }).ToList();
    }

    public void Insert(CourseInsertDTO dto)
    {
        var course = new Course()
        {
            Id = "BC/" + dto.BootcampId.ToString("0000") + "/" + dto.SkillId,
            TrainerId = dto.TrainerId,
            SkillId = dto.SkillId,
            BootcampClassId = dto.BootcampId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Progress = 2,
        };

        _repository.Insert(course);
    }

    public void Update(CourseUpdateDTO dto)
    {
        var course = _repository.GetById(dto.Id) ?? throw new Exception(ConstantConfigs.MESSAGE_NOT_FOUND("kursus"));
        course.EvaluationDate = dto.EvaluationDate;
        course.Progress = dto.Progress;

        _repository.Update(course);
    }

    public int Count(int bootcampId)
    {
        return _repository.Count(bootcampId);
    }
}
