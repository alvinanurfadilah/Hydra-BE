using System;
using HydraApi.DTOs.Categories;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/category")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet("allCategory")]
    public IActionResult Get()
    {
        try
        {
            var category = _service.Get();
            return Ok(category);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult Get(int pageNumber = 1, int pageSize = 5, string? name = "", string? level = "")
    {
        try
        {
            var category = _service.Get(pageNumber, pageSize, name, level);
            if (category.Count == 0)
            {
                return Ok(new ResponseWithPaginationDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("kategori"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                    Pagination = new PaginationDTO()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalData = _service.Count(name, level)
                    }
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<CategoryResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("kategori"),
                Status = ConstantConfigs.STATUS_OK,
                Data = category,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(name, level)
                }
            });
        }
        catch (System.Exception)
        {
            return BadRequest(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_FAILED,
                Status = ConstantConfigs.STATUS_FAILED
            });
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var category = _service.Get(id);
            if (category is null)
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("kandidat"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });

            return Ok(new ResponseDTO<CategoryResponseDTO>()
            {
                Message = ConstantConfigs.MESSAGE_GET("kategori"),
                Status = ConstantConfigs.STATUS_OK,
                Data = new CategoryResponseDTO()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Level = category.Level,
                    Description = category.Description,
                },
            });
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public IActionResult Insert(CategoryInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("kategpri"),
                Status = ConstantConfigs.STATUS_OK
            });
        }
        catch (System.Exception)
        {
            return BadRequest(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_FAILED,
                Status = ConstantConfigs.STATUS_FAILED
            });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(CategoryUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("kategori"),
                Status = ConstantConfigs.STATUS_OK
            });
        }
        catch (System.Exception)
        {
            return BadRequest(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_FAILED,
                Status = ConstantConfigs.STATUS_FAILED
            });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _service.Delete(id);
            return Ok();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
