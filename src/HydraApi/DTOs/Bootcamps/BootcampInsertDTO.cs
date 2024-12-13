using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Bootcamps;

public class BootcampInsertDTO
{
    public string? Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
