using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EveryDaily.Domain.Dto.Dto;
using EveryDaily.Domain.Entity;

namespace EveryDaily.Application.Mappings
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDto>().ReverseMap();            
        }
    }
}
