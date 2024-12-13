using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Courses;

public class CourseUpdateDTO
{
    public string Id { get; set; } = null!;
    public int Progress { get; set; }
    public DateTime EvaluationDate { get; set; }
}
