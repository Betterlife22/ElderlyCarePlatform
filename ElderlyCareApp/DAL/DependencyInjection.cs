using DAL.Extension;
using DAL.Interfaces;
using DAL.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            //Register repo
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Database Context
            var cnn = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(cnn)
            );


            return services;
        }
    }
}
