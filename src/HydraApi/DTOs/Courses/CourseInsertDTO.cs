using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Courses;

public class CourseInsertDTO
{
    [Required]
    public string SkillId { get; set; } = null!;
    [Required]
    public int TrainerId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    public int BootcampId { get; set; }
}
