using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class BaseFormEntity<T>
    {
        /// <summary>
        /// Thông tin đối tượng
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm
        /// </summary>
        public IFormFile File { get; set; }
    }
}
