using HydraDataAccess.Models;

namespace HydraBusiness.Interfaces;

public interface ICourseRepository
{
    List<Course> Get(int pageNumber, int pageSize, int bootcampId);
    List<Course> Get(int bootcampId);
    Course GetCourseByBootcampId(int bootcampId);
    Course GetById(string id);
    void Insert(Course course);
    void Update(Course course);
    int Count(int bootcampId);
}
