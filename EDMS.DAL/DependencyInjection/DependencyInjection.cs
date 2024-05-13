using EveryDaily.DAL.Interceptors;
using EveryDaily.DAL.Repositories;
using EveryDaily.Domain.Entity;
using EveryDaily.Domain.Interfaces.Repositories;
using EveryDaily.Domain.Interfaces.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EveryDaily.Application.Validations;

namespace EveryDaily.DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresSQL");            
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            serviceCollection.AddSingleton<DateInterceptor>();
            serviceCollection.RepositoriesInit();
            serviceCollection.ValidatorsRegister();
        }

        private static void RepositoriesInit(this IServiceCollection serviceCollection)
        {            
            serviceCollection.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            serviceCollection.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
        }

        private static void ValidatorsRegister(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReportValidator, ReportValidator>();
        }
    }
}
