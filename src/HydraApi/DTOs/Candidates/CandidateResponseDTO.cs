namespace HydraApi.DTOs.Candidates;

public class CandidateResponseDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public int BootcampId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Domicile { get; set; } = null!;
}
