using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Domain.Dto.Dto
{
    public record class CreateReportDto(string name, string description, long userId);
}
