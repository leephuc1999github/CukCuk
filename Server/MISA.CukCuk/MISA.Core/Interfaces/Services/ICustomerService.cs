using Microsoft.AspNetCore.Http;
using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Dịch vụ nhập dữ liệu
        /// </summary>
        /// <param name="file">File xlsx. dữ liệu</param>
        /// <returns></returns>
        public ServiceResult ImportCustomer(IFormFile file);


        /// <summary>
        /// DỊch vụ xuất dữ liệu
        /// </summary>
        /// <returns></returns>
        public ServiceResult ExportCustomer();
    }
}
