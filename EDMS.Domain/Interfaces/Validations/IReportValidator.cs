using EveryDaily.Domain.Entity;
using EveryDaily.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Domain.Interfaces.Validations
{
    public interface IReportValidator : IBaseValidator<Report>
    {/// <summary>
    /// Проверяется наличие отчета, если отчет с переданным названием есть в БД, создать такой же нельзя.
    /// Проверяется пользователь, если пользователь не найден, такого пользователя нет
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
        BaseResult CreateValidator(Report report, User user);
    }
}
