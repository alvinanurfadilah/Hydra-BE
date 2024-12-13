using System;
using HydraApi.DTOs.Trainers;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/trainer")]
[Authorize]
public class TrainerController : ControllerBase
{
    private readonly TrainerService _service;

    public TrainerController(TrainerService service)
    {
        _service = service;
    }

    [HttpGet("{skillId}")]
    public IActionResult Get(string skillId)
    {
        try
        {
            var trainer = _service.Get(skillId);
            return Ok(trainer);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }


    [HttpGet]
    public IActionResult Get(string? fullName, int pageNumber = 1, int pageSize = 5)
    {
        try
        {
            var trainer = _service.Get(pageNumber, pageSize, fullName);
            if (trainer.Count == 0)
            {
                return Ok(new ResponseWithPaginationDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("trainer"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                    Pagination = new PaginationDTO()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalData = _service.Count(fullName)
                    }
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<TrainerResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("trainer"),
                Status = ConstantConfigs.STATUS_OK,
                Data = trainer,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(fullName)
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

    [HttpGet("getById/{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var trainer = _service.Get(id);
            if (trainer is null)
            {
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("trainer"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });
            }

            return Ok(new ResponseDTO<TrainerResponseDTO>()
            {
                Message = ConstantConfigs.MESSAGE_GET("skill"),
                Status = ConstantConfigs.STATUS_OK,
                Data = new TrainerResponseDTO()
                {
                    Id = trainer.Id,
                    Username = trainer.Username,
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,
                    BirthDate = trainer.BirthDate,
                    Gender = trainer.Gender,
                    PhoneNumber = trainer.PhoneNumber,
                    IsAvailable = trainer.IsAvailable,
                    IsActive = trainer.IsActive,
                },
            });
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public IActionResult Insert(TrainerInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("trainer"),
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
    public IActionResult Update(TrainerUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("trainer"),
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
