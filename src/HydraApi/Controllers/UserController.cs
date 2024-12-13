using HydraApi.DTOs.Users;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var result = _service.Get();
        return Ok(result);
    }

    [HttpGet]
    public IActionResult Get(string? username, int pageNumber = 1, int pageSize = 5)
    {
        // try
        // {
            var user = _service.Get(pageNumber, pageSize, username);
            if (user.Count == 0)
            {
                return Ok(new ResponseWithPaginationDTO<string[]>()
                {
                    Message = ConstantConfigs.MESSAGE_NOT_FOUND("user"),
                    Status = ConstantConfigs.STATUS_NOT_FOUND,
                    Data = Array.Empty<string>(),
                    Pagination = new PaginationDTO()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalData = _service.Count(username)
                    }
                });
            }

            return Ok(new ResponseWithPaginationDTO<List<UserResponseDTO>>()
            {
                Message = ConstantConfigs.MESSAGE_GET("user"),
                Status = ConstantConfigs.STATUS_OK,
                Data = user,
                Pagination = new PaginationDTO()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalData = _service.Count(username)
                }
            });
        // }
        // catch (System.Exception)
        // {
        //     return BadRequest(new ResponseDTO<string>()
        //     {
        //         Message = ConstantConfigs.MESSAGE_FAILED,
        //         Status = ConstantConfigs.STATUS_FAILED
        //     });
        // }
    }

    [HttpGet("{username}")]
    public IActionResult Get(string username)
    {
        var result = _service.Get(username);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Register(UserRegisterDTO dto)
    {
        try
        {
            _service.Insert(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_POST("user"),
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

    [HttpPut("{username}")]
    public IActionResult Update(UserUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);

            return Ok(new ResponseDTO<string>()
            {
                Message = ConstantConfigs.MESSAGE_PUT("user"),
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

    [HttpDelete("{username}")]
    public IActionResult Delete(string username)
    {
        try
        {
            _service.Delete(username);
            return Ok();
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}