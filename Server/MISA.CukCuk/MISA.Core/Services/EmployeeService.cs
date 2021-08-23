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
                var entities = _baseRepository.GetByProperty(name, value);
                serviceResult.SetSuccess(serviceResult, entities);
                if (entities.Count() > 0)
                {
                    serviceResult.UserMessage = Properties.Resource.User_Success_Msg;
                    serviceResult.DevMessage.Add(Properties.Resource.User_Success_Msg);
                }
                else
                {
                    serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
                    serviceResult.DevMessage.Add(Properties.Resource.Dev_Error_Msg);
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
                if(employees.Data.Count() > 0)
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

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ServiceResult DeleteEmployees(string ids)
        {
            ServiceResult resultService = new ServiceResult();
            try
            {
                string[] parts = ids.Split(",");
                List<Guid> keys = new List<Guid>();
                bool flag = true;
                for (int i = 0; i < parts.Length; i++)
                {
                    Guid temp = new Guid();
                    if(Guid.TryParse(parts[i], out temp))
                    {
                        keys.Add(temp);
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    var result = _employeeRepository.DeleteMultiEmployees(keys);
                    if (!result)
                    {
                        resultService.SetBadRequest(resultService);
                        resultService.DevMessage.Add("EmployeeId sai hoặc không tồn tại");
                    }
                }
                else
                {
                    resultService.SetBadRequest(resultService);
                    resultService.DevMessage.Add("EmployeeId sai hoặc không tồn tại");
                }
            }
            catch (Exception ex)
            {
                resultService.SetInternalServerError(resultService);
                resultService.DevMessage.Add(ex.Message);
            }
            return resultService;
        }
        #endregion
    }
}
