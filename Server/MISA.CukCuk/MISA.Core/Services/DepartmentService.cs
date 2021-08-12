using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    /// <summary>
    /// Dịch vụ lớp phòng ban
    /// </summary>
    /// CreatedBy : LP(12/8)
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
              
        }
        #endregion
    }
}
