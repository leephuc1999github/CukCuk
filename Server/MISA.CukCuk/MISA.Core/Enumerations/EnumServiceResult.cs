using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enumerations
{
    /// <summary>
    /// Trạng thái phản hồi khi request
    /// </summary>
    public enum EnumServiceResult
    {
        Success = 200,
        Created = 201,
        NoContent = 204,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }
}
