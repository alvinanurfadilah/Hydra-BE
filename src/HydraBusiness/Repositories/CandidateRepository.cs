using HydraBusiness.Interfaces;
using HydraDataAccess.Models;

namespace HydraBusiness.Repositories;

public class CandidateRepository : ICandidateRepository
{
    private readonly HydraContext _context;

    public CandidateRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Candidate> Get(int pageNumber, int pageSize, string fullName, int bootcampId)
    {
        return _context.Candidates
        .Where(can => (can.FirstName + can.LastName).ToLower().Contains(fullName??"".ToLower()) && (can.BootcampClassId == bootcampId || 0 == bootcampId))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public Candidate GetById(int id)
    {
        return _context.Candidates.Find(id);
    }

    public void Insert(Candidate candidate)
    {
        try
        {
            var getProgress = _context.BootcampClasses.Find(candidate.BootcampClassId);
            if (getProgress.Progress != 2)
            {
                _context.Candidates.Add(candidate);
                _context.SaveChanges();
            } else
            {
                throw new Exception();
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Update(Candidate candidate)
    {
        try
        {
            var getProgress = _context.BootcampClasses.Find(candidate.BootcampClassId);
            if (getProgress.Progress != 2)
            {
                _context.Candidates.Update(candidate);
                _context.SaveChanges();
            } else
            {
                throw new Exception();
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public int Count(string fullName, int bootcampId)
    {
        return _context.Candidates
        .Where(can => (can.FirstName + can.LastName).ToLower().Contains(fullName??"".ToLower()) && (can.BootcampClassId == bootcampId || 0 == bootcampId))
        .Count();
    }

    public int Count(int bootcampId)
    {
        return _context.Candidates
        .Where(can => can.BootcampClassId == bootcampId)
        .Count();
    }
}
