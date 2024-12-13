using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Bootcamps;

public class BootcampUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
