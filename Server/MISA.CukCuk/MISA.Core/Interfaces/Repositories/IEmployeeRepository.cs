using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="posistionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public BaseEntityPaging<Employee> GetEmployeesPaging(string keyword, string posistionId, string departmentId, int pageIndex, int pageSize);

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteMultiEmployees(List<Guid> ids);
    }
}
