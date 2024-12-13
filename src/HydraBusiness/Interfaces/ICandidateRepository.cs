using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ICandidateRepository
{
    List<Candidate> Get(int pageNumber, int pageSize, string fullName, int bootcampId);
    Candidate GetById(int id);
    void Insert(Candidate candidate);
    void Update(Candidate candidate);
    int Count(string fullName, int bootcampId);
    int Count(int bootcampId);
}
