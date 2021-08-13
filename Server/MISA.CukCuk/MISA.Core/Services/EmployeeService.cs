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
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            this._employeeRepository = employeeRepository;
        }


        #endregion

        #region Method
        /// <summary>
        /// Trả về service khi lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetNewEmployeeCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var employee = _employeeRepository.GetNewEmployeeCode();
                if(employee != null)
                {
                    serviceResult.SetSuccess(serviceResult, employee);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception: {ex.Message}");
            }
            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra giá trị trừng
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceResult GetEmployeeByProperty(string name, string value)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var entity = _baseRepository.GetByProperty(name, value);
                serviceResult.SetSuccess(serviceResult, entity);
                if (entity != null)
                {
                    serviceResult.UserMessage = $"Giá trị {value} đã tồn tại";
                    serviceResult.DevMessage.Add($"Giá trị {value} đã tồn tại");
                }
                else
                {
                    serviceResult.UserMessage = $"Giá trị {value} chưa có trong csdl";
                    serviceResult.DevMessage.Add($"Giá trị {value} chưa có trong csdl");
                }
                
            }
            catch(Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception: {ex.Message}");
            }
            return serviceResult;
        }

        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="posistionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ServiceResult GetEmployeesPaging(string keyword, string posistionId, string departmentId, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var employees = _employeeRepository.GetEmployeesPaging(keyword, posistionId, departmentId, pageIndex, pageSize);
                if(employees.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, employees);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }
        #endregion
    }
}
