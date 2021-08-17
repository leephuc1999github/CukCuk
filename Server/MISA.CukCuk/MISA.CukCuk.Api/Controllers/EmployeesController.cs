using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Declare
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            this._employeeService = employeeService;
        }
        #endregion

        /// <summary>
        /// Phân trang và tìm kiếm
        /// </summary>
        /// <param name="keyword">tên số điện thoại , mã nhân viên</param>
        /// <param name="pageIndex">Trang </param>
        /// <param name="pageSize">Độ dài trang</param>
        /// <param name="positionId">Chức vụ id</param>
        /// <param name="departmentId">Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        [HttpGet]
        [Route("paging")]
        public IActionResult GetPaging(string keyword, int pageIndex, int pageSize, string positionId, string departmentId)
        {
            var employees = _employeeService.GetEmployeesPaging(keyword, positionId, departmentId, pageIndex, pageSize);
            return Ok(employees);
        }

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var employees = _employeeService.GetAll();
        //    return Ok(employees);
        //}

        /// <summary>
        /// Lấy nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        //[HttpGet("{id}")]
        //public IActionResult GetById(Guid id)
        //{
        //    var employee = _employeeService.GetById(id);
        //    return Ok(employee);
        //}


        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân vien</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        //[HttpPost]
        //public IActionResult Insert([FromBody] Employee employee)
        //{
        //    var rowEffects = _employeeService.Insert(employee);
        //    return Ok(rowEffects);
        //}

        /// <summary>
        /// Cập nhật một nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <param name="employee">Thông tin cập nhật nhân viên</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        //[HttpPut("{id}")]
        //public IActionResult Update(Guid id, [FromBody] Employee employee)
        //{
        //    ServiceResult serviceResult = _employeeService.Update(id, employee);
        //    return Ok(serviceResult);
        //}

        /// <summary>
        /// Xóa một nhân viên theo id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns></returns>
        /// CreatedBy LP(12/8)
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    ServiceResult serviceResult = _employeeService.Delete(id);
        //    return Ok(serviceResult);
        //}

        /// <summary>
        /// Tạo mã nhân viên mưới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        [HttpGet]
        [Route("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            ServiceResult serviceResult = _employeeService.GetNewEmployeeCode();
            return Ok(serviceResult);
        }

        /// <summary>
        /// Lấy nhân viên theo cột dữ liệu và giá trị
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="value">Giá trị cootj</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        //[HttpGet]
        //[Route("Property")]
        //public IActionResult GetEmployeeByProperty(string name, string value)
        //{
        //    ServiceResult serviceResult = _employeeService.GetEmployeeByProperty(name, value);
        //    return Ok(serviceResult);
        //}

    }

    

}
