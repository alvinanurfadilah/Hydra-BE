using System;
using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Skills;

public class SkillUpdateDTO
{
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
}
