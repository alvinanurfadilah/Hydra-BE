namespace HydraApi.DTOs.Trainers;

public class TrainerResponseDTO
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Gender { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public bool? IsAvailable { get; set; }
    public bool? IsActive { get; set; }
}