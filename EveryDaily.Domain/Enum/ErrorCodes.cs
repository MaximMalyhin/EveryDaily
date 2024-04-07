using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryDaily.Domain.Enum
{
    public enum ErrorCodes
    {
        ItemsNotFound = 0,
        ItemNotFound = 1,
        InternalServerError = 10,
        ItemExists = 11,
    }
}
