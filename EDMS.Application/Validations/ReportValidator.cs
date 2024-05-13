using EveryDaily.Application.Resources;
using EveryDaily.Domain.Dto.Dto;
using EveryDaily.Domain.Entity;
using EveryDaily.Domain.Enum;
using EveryDaily.Domain.Interfaces.Validations;
using EveryDaily.Domain.Result;

namespace EveryDaily.Application.Validations
{
    public class ReportValidator : IReportValidator
    {
        public BaseResult CreateValidator(Report report, User user)
        {
            if(report != null)            
                return new BaseResult()
                {
                    ErrorCode = (int)ErrorCodes.ItemExists,
                    ErrorMessage = ErrorMessage.ItemExists
                };

            if (user == null)
                return new BaseResult()
                {
                    ErrorCode = (int)ErrorCodes.ItemNotFound,
                    ErrorMessage = ErrorMessage.ItemNotFound
                };

            return new BaseResult();
        }

        public BaseResult ValidateOnNull(Report Entity)
        {
            if (Entity == null) return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ItemNotFound,
                ErrorCode = (int)ErrorCodes.ItemNotFound,
            };
            return new BaseResult();
        }
    }
}
