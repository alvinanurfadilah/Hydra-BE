using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ITrainerRepository
{
    List<Trainer> Get();
    List<Trainer> Get(string skillId);

    List<Trainer> Get(int pageNumber, int pageSize, string fullName);
    Trainer Get(int id);
    void Insert(Trainer trainer);
    void Update(Trainer trainer);
    void Delete(Trainer trainer);
    int Count(string fullName);
}
