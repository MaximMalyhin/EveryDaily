using EveryDaily.Application.Mappings;
using EveryDaily.Application.Services;
using EveryDaily.Application.Validations.FluentValidations.Report;
using EveryDaily.Domain.Dto.Dto;
using EveryDaily.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EveryDaily.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportMapping));
            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        }
    }
}
