using EveryDaily.Domain.Dto.Dto;
using EveryDaily.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Domain.Interfaces.Services
{

    /// <summary>
    /// Сервис отвечает за работу с доменной частью отчета Report
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получение всех отчетов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
        /// <summary>
        /// Получение отчета по идентефикатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
        /// <summary>
        /// Создание репорта 
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto createDto);
        /// <summary>
        /// Удаление отчета по идентефикатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> DeleteReportAsync(long id);
        /// <summary>
        /// Обновление отчета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDto>> UpdateReportAsync(long id, UpdateReportDto updateDto);
    }
}
