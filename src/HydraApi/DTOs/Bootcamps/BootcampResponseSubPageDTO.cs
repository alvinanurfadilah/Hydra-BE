namespace HydraApi;

public class BootcampResponseSubPageDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int TotalCandidate { get; set; }
    public string TrainerName { get; set; } = null!;
    public string SkillName { get; set; } = null!;
}
