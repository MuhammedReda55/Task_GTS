using Microsoft.EntityFrameworkCore;
using Task_GTS.Data;
using Task_GTS.Repository;
using Task_GTS.Repository.IRepository;

namespace Task_GTS.Extensions
{
    public static class UpWorkServiceExtensions
    {
        public static IServiceCollection AddUpWorkServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            return services;
        }
    }
}

