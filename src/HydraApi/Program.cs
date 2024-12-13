using System.Text;
using HydraDataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace HydraApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        IServiceCollection services = builder.Services;
        ConfigurationManager configuration = builder.Configuration;
        Dependencies.ConfigureService(configuration, services);
        services.AddBusinessService();
        services.AddAuthorization();
        services.AddControllers();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option => 
        {
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value??"")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Hydra API"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Enter the token with the `Bearer: ` prefix, e.g. 'Bearer fdhauy837r3'",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        services.AddCors(options => 
        {
            options.AddPolicy(name: MyAllowSpecificOrigins, policy => 
            {
                policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
            });
        });
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(MyAllowSpecificOrigins);
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(MyAllowSpecificOrigins);

        app.Run();
    }
}