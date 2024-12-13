using HydraApi.DTOs.Bootcamps;
using HydraApi.Services;
using HydraDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/bootcamp")]
[Authorize]
public class BootcampController : ControllerBase
{
    private readonly BootcampService _service;

    public BootcampController(BootcampService service)
    {
        _service = service;
    }

    [HttpGet("allBootcamp")]
    public IActionResult GetAllBootcamp()
    {
        try
        {
           var bootcamp = _service.Get();
           return Ok(bootcamp);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public IActionResult Get(int id, int pageNumber = 1, int pageSize = 5)
    {
        try
        {
            var bootcamp = _service.Get(pageNumber, pageSize, id);
            if (bootcamp.Count == 0)
            {
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("bootcamp"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<BootcampResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("bootcamp"),
                Status = ConstantConfigs.STATUS_OK,
                Data = bootcamp,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(id)
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
    public IActionResult GetById(int id)
    {
        try
        {
            var bc = _service.GetById(id);
            if (bc is null)
                return NotFound(new ResponseDTO<string[]>(){
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("bootcamp"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });

            return Ok(new ResponseDTO<BootcampResponseDTO>(){
                Message = ConstantConfigs.MESSAGE_GET("bootcamp"),
                Status = ConstantConfigs.STATUS_OK,
                Data = new BootcampResponseDTO(){
                    Id = bc.Id,
                    Description = bc.Description,
                    StartDate = bc.StartDate,
                    EndDate = bc.EndDate,
                    Progress = _service.GetProgress(bc.Progress)
                },
            });
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    [HttpPost]
    public IActionResult Insert(BootcampInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("bootcamp"),
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
    public IActionResult Update(BootcampUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("bootcamp"),
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

    [HttpPut("plan/{id}")]
    public IActionResult UpdatePlan(BootcampUpdatePlanDTO dto)
    {
        try
        {
            _service.UpdatePlan(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("bootcamp plan"),
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

    [HttpGet("subpage")]
    public IActionResult GetSubPage(int id, int progress = 1, int pageNumber = 1, int pageSize = 5)
    {
        try
        {
            var bootcamp = _service.GetSubPage(pageNumber, pageSize, id, progress);
            if (bootcamp.Count == 0)
            {
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("bootcamp"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<BootcampResponseSubPageDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("bootcamp"),
                Status = ConstantConfigs.STATUS_OK,
                Data = bootcamp,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.CountCandidate(id, progress)
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
}
