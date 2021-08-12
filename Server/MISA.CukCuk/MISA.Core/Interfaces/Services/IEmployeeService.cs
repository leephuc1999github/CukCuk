﻿using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Interface nhân viên
    /// </summary>
    /// CreatedBy : LP(6/8)
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Tạo mã nhân viên mưới
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetNewEmployeeCode();

        /// <summary>
        /// Kiểm tra đầu vào dữ liệu trùng
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ServiceResult GetEmployeeByProperty(string name, string value);
    }
}
