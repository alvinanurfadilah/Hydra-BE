namespace HydraApi.DTOs.Trainers;

public class TrainerInsertDTO
{
    public string? Username { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public int? IsAvailable { get; set; }
    public int? IsActive { get; set; }

    // public List<string> Skills { get; set; }
}