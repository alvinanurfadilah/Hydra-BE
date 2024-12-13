using System;

namespace HydraApi.DTOs.Candidates;

public class CandidateDTO
{
    public int Id { get; set; }
    public int BootcampClassId { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Domicile { get; set; } = null!;
    public bool HasPassed { get; set; }
    public bool? IsActive { get; set; }
}
