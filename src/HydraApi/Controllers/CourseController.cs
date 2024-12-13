using HydraApi.DTOs.Courses;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/course")]
[Authorize]
public class CourseController : ControllerBase
{
    private readonly CourseService _service;

    public CourseController(CourseService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(int bootcampId, int pageNumber = 1, int pageSize = 5)
    {
        try
        {
            var course = _service.Get(pageNumber, pageSize, bootcampId);
            if (course.Count == 0)
            {
                return NotFound(new ResponseDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("kursus"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>()
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<CourseResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("kursus"),
                Status = ConstantConfigs.STATUS_OK,
                Data = course,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(bootcampId)
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

    [HttpGet("{bootcampId}")]
    public IActionResult GetByBootcampId(int bootcampId)
    {
        try
        {
            var course = _service.GetByBootcampId(bootcampId);
            return Ok(course);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult Insert(CourseInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("kursus"),
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
    public IActionResult Update(CourseUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("kursus"),
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
