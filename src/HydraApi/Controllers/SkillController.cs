using HydraApi.DTOs.Skills;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/skill")]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly SkillService _service;

    public SkillController(SkillService service)
    {
        _service = service;
    }

    [HttpGet("allSkill")]
    public IActionResult Get()
    {
        try
        {
            var skill = _service.Get();
            return Ok(skill);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult Get(int categoryId, int pageNumber = 1, int pageSize = 5)
    {
        try
        {
            var skill = _service.Get(pageNumber, pageSize, categoryId);
            if (skill.Count == 0)
            {
                return Ok(new ResponseWithPaginationDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("skill"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                    Pagination = new PaginationDTO()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalData = _service.Count(categoryId)
                    }
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<SkillResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("skill"),
                Status = ConstantConfigs.STATUS_OK,
                Data = skill,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(categoryId)
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
    public IActionResult Get(string id)
    {
        try
        {
            var skill = _service.Get(id);
            if (skill is null)
            {
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("skill"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });
            }

            return Ok(new ResponseDTO<SkillUpdateDTO>()
            {
                Message = ConstantConfigs.MESSAGE_GET("skill"),
                Status = ConstantConfigs.STATUS_OK,
                Data = new SkillUpdateDTO()
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Description = skill.Description,
                    CategoryId = skill.CategoryId,
                },
            });
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public IActionResult Insert(SkillInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("skill"),
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
    public IActionResult Update(SkillUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("skill"),
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
    public IActionResult Delete(string id)
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
