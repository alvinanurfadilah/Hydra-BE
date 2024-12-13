namespace HydraApi.DTOs.Bootcamps;

public class BootcampResponseDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int CountCandidate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Progress { get; set; }
    public string TrainerName { get; set; }
    public string SkillName { get; set; }
}
