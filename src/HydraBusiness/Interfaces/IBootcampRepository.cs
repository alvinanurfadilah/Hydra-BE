using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface IBootcampRepository
{
    List<BootcampClass> Get(int pageNumber, int pageSize, int id);
    List<BootcampClass> Get();
    BootcampClass GetById(int id);
    void Insert(BootcampClass bootcampClass);
    void Update(BootcampClass bootcampClass);
    int Count(int id);

    List<BootcampClass> GetSubPage(int pageNumber, int pageSize,int id, int progress);
    int CountCandidate(int id, int progress);
}
