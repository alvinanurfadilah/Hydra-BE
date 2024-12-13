using HydraApi.DTOs.Candidates;
using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraApi.Services;

public class CandidateService
{
    private readonly ICandidateRepository _repository;

    public CandidateService(ICandidateRepository repository)
    {
        _repository = repository;
    }

    public List<CandidateResponseDTO> Get(int pageNumber, int pageSize, string fullName, int bootcampId)
    {
        return _repository.Get(pageNumber, pageSize, fullName, bootcampId)
        .Select(can => new CandidateResponseDTO()
        {
            Id = can.Id,
            FullName = can.FirstName + " " + can.LastName,
            BootcampId = can.BootcampClassId,
            PhoneNumber = can.PhoneNumber,
            Domicile = can.Domicile
        }).ToList();
    }

    public Candidate GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Insert(CandidateInsertDTO dto)
    {
        var candidate = new Candidate()
        {
            BootcampClassId = dto.BootcampClassId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            Address = dto.Address,
            Domicile = dto.Domicile,
            PhoneNumber = dto.PhoneNumber
        };

        _repository.Insert(candidate);
    }

    public void Update(CandidateUpdateDTO dto)
    {
        var candidate = _repository.GetById(dto.Id);
        candidate.BootcampClassId = dto.BootcampClassId;
        candidate.FirstName = dto.FirstName;
        candidate.LastName = dto.LastName;
        candidate.Gender = dto.Gender;
        candidate.BirthDate = dto.BirthDate;
        candidate.Address = dto.Address;
        candidate.Domicile = dto.Domicile;

        _repository.Update(candidate);
    }

    public int Count(string fullName, int bootcampId)
    {
        return _repository.Count(fullName, bootcampId);
    }
}
