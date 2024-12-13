namespace HydraApi.DTOs.Courses;

public class CourseResponseDTO
{
    public string Id { get; set; }
    public string Skill { get; set; }
    public string Trainer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Progress { get; set; }
    public DateTime? EvaluationDate { get; set; }
}
