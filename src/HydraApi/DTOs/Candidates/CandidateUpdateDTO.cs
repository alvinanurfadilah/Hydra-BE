using System.ComponentModel.DataAnnotations;

namespace HydraApi.DTOs.Candidates;

public class CandidateUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int BootcampClassId { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public string Domicile { get; set; } = null!;
}