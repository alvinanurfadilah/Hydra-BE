namespace HydraApi.DTOs.Bootcamps;

public class BootcampUpdatePlanDTO
{
    public int Id { get; set; }
    public int Progress { get; set; }

    public DateTime? EndDate { get; set; }
}
