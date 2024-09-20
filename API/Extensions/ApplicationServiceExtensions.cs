using Application.Activities;
using Application.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(
                opt =>
                {
                    opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                }
            );

            services.AddCors(opt =>
            {
                opt.AddPolicy("Policy", policy => policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"));
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));    
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();


            return services;
        }
    }
}