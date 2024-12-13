using System;
using HydraApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HydraApi.Controllers;

[ApiController]
[Route("api/v1/trainer")]
[Authorize]
public class TrainerSkillDetailController : ControllerBase
{
    private readonly TrainerSkillDetailService _service;

    public TrainerSkillDetailController(TrainerSkillDetailService service)
    {
        _service = service;
    }

    [HttpDelete]
    public IActionResult Delete(int trainerId, string skillId)
    {
        _service.Delete(trainerId, skillId);
        return Ok();
    }
}
