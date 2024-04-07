using EveryDaily.Domain.Dto.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Application.Validations.FluentValidations.Report
{
    public class CreateReportValidator : AbstractValidator<CreateReportDto>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.description).NotEmpty().MaximumLength(1000);
        }
    }
}
