using HydraApi.DTOs.Candidates;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/candidate")]
[Authorize]
public class CandidateController : ControllerBase
{
    private readonly CandidateService _service;

    public CandidateController(CandidateService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(int bootcampId, int pageNumber = 1, int pageSize = 5, string? fullName = "")
    {
        try
        {
            var candidate = _service.Get(pageNumber, pageSize, fullName, bootcampId);
            if (candidate.Count == 0)
            {
                return Ok(new ResponseWithPaginationDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("kandidat"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                    Pagination = new PaginationDTO() {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalData = _service.Count(fullName, bootcampId)
                    }
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<CandidateResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("kandidat"),
                Status = ConstantConfigs.STATUS_OK,
                Data = candidate,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(fullName, bootcampId)
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
            var candidate = _service.GetById(id);
            if (candidate is null)
                return NotFound(new ResponseDTO<string[]>(){
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("candidate"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });

            return Ok(new ResponseDTO<CandidateDTO>(){
                Message = ConstantConfigs.MESSAGE_GET("bootcamp"),
                Status = ConstantConfigs.STATUS_OK,
                Data = new CandidateDTO(){
                    Id = candidate.Id,
                    BootcampClassId = candidate.BootcampClassId,
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    BirthDate = candidate.BirthDate,
                    Gender = candidate.Gender,
                    PhoneNumber = candidate.PhoneNumber,
                    Address = candidate.Address,
                    Domicile = candidate.Domicile,
                    HasPassed = candidate.HasPassed,
                    IsActive = candidate.IsActive
                },
            });

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    [HttpPost]
    public IActionResult Insert(CandidateInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("kandidat"),
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
            throw;
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(CandidateUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("kandidat"),
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
}
