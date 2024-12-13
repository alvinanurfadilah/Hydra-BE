using System;
using HydraApi.DTOs.Categories;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public List<CategoryResponseDTO> Get()
    {
        return _repository.Get().Select(cat => new CategoryResponseDTO()
        {
            Id = cat.Id,
            Name= cat.Name,
            Description = cat.Description
        }).ToList();
    }

    public List<CategoryResponseDTO> Get(int pageNumber, int pageSize, string name, string level)
    {
        return _repository.Get(pageNumber, pageSize, name, level).Select(cat => new CategoryResponseDTO()
        {
            Id = cat.Id,
            Name = cat.Name,
            Level = cat.Level,
            Description = cat.Description
        }).ToList();
    }

    public Category Get(int id)
    {
        return _repository.Get(id);
    }

    public void Insert(CategoryInsertDTO dto)
    {
        var model = new Category()
        {
            Name = dto.Name,
            Level = dto.Level,
            Description = dto.Description
        };

        _repository.Insert(model);
    }

    public void Update(CategoryUpdateDTO dto)
    {
        var model = _repository.Get(dto.Id);

        model.Name = dto.Name;
        model.Level = dto.Level;
        model.Description = dto.Description;

        _repository.Update(model);
    }

    public void Delete(int id)
    {
        var model = _repository.Get(id);
        _repository.Delete(model);
    }

    public int Count(string name, string level)
    {
        return _repository.Count(name, level);
    }
}
