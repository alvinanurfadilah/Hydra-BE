using HydraBusiness.Interfaces;
using HydraDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HydraBusiness.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly HydraContext _context;

    public CourseRepository(HydraContext context)
    {
        _context = context;
    }

    public List<Course> Get(int PageNumber, int PageSize, int bootcampId)
    {
        return _context.Courses
        .Include(c => c.TrainerSkillDetail)
        .ThenInclude(tsd => tsd.Trainer)
        .Include(c => c.TrainerSkillDetail)
        .ThenInclude(tsd => tsd.Skill)
        .Where(c => c.BootcampClassId == bootcampId)
        .Skip((PageNumber - 1) * PageSize)
        .Take(PageSize)
        .ToList();
    }

    public List<Course> Get(int bootcampId)
    {
        return _context.Courses
        .Include(c => c.TrainerSkillDetail)
        .ThenInclude(tsd => tsd.Trainer)
        .Include(c => c.TrainerSkillDetail)
        .ThenInclude(tsd => tsd.Skill)
        .Where(c => c.BootcampClassId == bootcampId)
        .ToList();
    }

    public Course GetCourseByBootcampId(int bootcampId)
    {
        return _context.Courses
            .Include(c => c.TrainerSkillDetail)
            .ThenInclude(tsd => tsd.Trainer)
            .Include(c => c.TrainerSkillDetail)
            .ThenInclude(tsd => tsd.Skill)
            .Where(c => c.BootcampClassId == bootcampId)
            .OrderByDescending(c => c.StartDate)
            .FirstOrDefault();
    }

    public Course GetById(string id)
    {
        return _context.Courses.Find(id);
    }

    public void Insert(Course course)
    {
        try
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Update(Course course)
    {
        try
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public int Count(int bootcampId)
    {
        return _context.Courses
        .Where(c => c.BootcampClassId == bootcampId)
        .Count();
    }
}
