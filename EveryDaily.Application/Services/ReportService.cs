using AutoMapper;
using EveryDaily.Application.Resources;
using EveryDaily.Application.Validations;
using EveryDaily.Domain.Dto.Dto;
using EveryDaily.Domain.Entity;
using EveryDaily.Domain.Enum;
using EveryDaily.Domain.Interfaces.Repositories;
using EveryDaily.Domain.Interfaces.Services;
using EveryDaily.Domain.Interfaces.Validations;
using EveryDaily.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IReportValidator _reportValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository, IReportValidator reportValidator, ILogger logger)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _reportValidator = reportValidator;
            _logger = logger;
        }
        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto createDto)
        {
            User user;
            Report report;

            try
            {
                user = await _userRepository.GetAll().FirstAsync(x => x.Id == createDto.userId);
                report = await _reportRepository.GetAll().FirstAsync(x => x.Name == createDto.name);  
                
                var result = _reportValidator.CreateValidator(report, user);

                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                }

                Report newReport = new Report()
                {
                    Name = createDto.name,
                    Description = createDto.description,
                    UserId = user.Id
                };

                await _reportRepository.CreateAsync(newReport);

                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(newReport)
                };
            }            
            catch(Exception ex)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }            
        }
        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstAsync(x => x.Id == id);

                var result = _reportValidator.ValidateOnNull(report);

                if (result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorCode = result.ErrorCode,
                        ErrorMessage = result.ErrorMessage,
                        Data = _mapper.Map<ReportDto>(report)
                    };
                }

                await _reportRepository.RemoveAsync(report);

                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch(Exception ex)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
        /// <inheritdoc />

        public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
        {
            Report report = null;
            BaseResult<ReportDto> baseResult;

            try
            {
                report = await _reportRepository.GetAll().SingleAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                baseResult = new BaseResult<ReportDto>
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }

            if(report == null)
            {
                _logger.Warning("Отчет с {0} не найден", id);
                return new BaseResult<ReportDto>
                {
                    ErrorCode = (int)ErrorCodes.ItemNotFound,
                    ErrorMessage = ErrorMessage.ItemNotFound.ToString()
                };
            }

            baseResult = new BaseResult<ReportDto>()
            {
                Data = new ReportDto(report.Id, report.Name, report.Description, report.CreatedAt.ToLongDateString())
            };

            return baseResult;
        }

        /// <inheritdoc />
        public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
        {
            ReportDto[] reports;
            try
            {
                reports = await _reportRepository.
                    GetAll().
                    Where(x => x.UserId == userId).
                    Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString())).
                    ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ReportDto>()
                {
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                    ErrorMessage = ErrorMessage.InternalServerError.ToString()
                };
            }

            if (!reports.Any()) 
            {
                _logger.Warning(ErrorMessage.ItemsNotFound, reports.Length);
                return new CollectionResult<ReportDto>()
                {
                    ErrorCode = (int)ErrorCodes.ItemsNotFound,
                    ErrorMessage = ErrorMessage.ItemsNotFound.ToString()
                };
            }

            return new CollectionResult<ReportDto>()
            {
                Data = reports,
                Count = reports.Length
            };
        }
        /// <inheritdoc />

        public async Task<BaseResult<ReportDto>> UpdateReportAsync(long id, UpdateReportDto updateDto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstAsync(x => x.Id == id);

                var result = _reportValidator.ValidateOnNull(report);

                if (result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorCode = result.ErrorCode,
                        ErrorMessage = result.ErrorMessage,
                        Data = _mapper.Map<ReportDto>(report)
                    };
                }

                Report updateReport = new Report()
                {
                    Id = updateDto.Id,
                    Name = updateDto.Name,
                    Description = updateDto.Description
                };

                await _reportRepository.UpdateAsync(report);

                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }
}
