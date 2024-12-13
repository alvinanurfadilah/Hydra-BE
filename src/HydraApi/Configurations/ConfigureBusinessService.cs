using HydraApi.Services;
using HydraBusiness.Interfaces;
using HydraBusiness.Repositories;

namespace HydraApi;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IBootcampRepository, BootcampRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITrainerSkillDetailRepository, TrainerSkillDetailRepository>();
        
        
        services.AddScoped<CandidateService>();
        services.AddScoped<BootcampService>();
        services.AddScoped<TrainerService>();
        services.AddScoped<CourseService>();
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<SkillService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<TrainerSkillDetailService>();
        return services;
    }
}