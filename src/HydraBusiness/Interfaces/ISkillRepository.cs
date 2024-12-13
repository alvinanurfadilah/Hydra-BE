using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ISkillRepository
{
    List<Skill> Get();
    List<Skill> Get(int trainerId);

    List<Skill> Get(int pageNumber, int pageSize, int categoryId);
    Skill GetById(string id);
    void Insert(Skill skill);
    void Update(Skill skill);
    void Delete(Skill skill);
    int Count(int categoryId);
}
