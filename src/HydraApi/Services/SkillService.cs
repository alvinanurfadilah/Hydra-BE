using HydraApi.DTOs.Skills;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class SkillService
{
    private readonly ISkillRepository _repository;

    public SkillService(ISkillRepository repository)
    {
        _repository = repository;
    }

    public List<SkillResponseDTO> Get()
    {
        return _repository.Get().Select(course => new SkillResponseDTO()
        {
            Id = course.Id,
            Name= course.Name,
            Description = course.Description
        }).ToList();
    }

    
    public List<SkillResponseDTO> Get(int pageNumber, int pageSize, int categoryId)
    {
        return _repository.Get(pageNumber, pageSize, categoryId).Select(skill => new SkillResponseDTO()
        {
            Id = skill.Id,
            Name = skill.Name,
            Description = skill.Description,
            Category = skill.Category.Name
        }).ToList();
    }

    public Skill Get(string id)
    {
        return _repository.GetById(id);
    }

    public void Insert(SkillInsertDTO dto)
    {
        var model = new Skill()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId
        };

        _repository.Insert(model);
    }

    public void Update(SkillUpdateDTO dto)
    {
        var model = _repository.GetById(dto.Id);
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.CategoryId = dto.CategoryId;

        _repository.Update(model);
    }

    public void Delete(string id)
    {
        var model = _repository.GetById(id);
        _repository.Delete(model);
    }

    public int Count(int categoryId)
    {
        return _repository.Count(categoryId);
    }
}