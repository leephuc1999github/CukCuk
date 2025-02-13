﻿using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Service generic interface
    /// CreatedBy : LP(6/8)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        #region Method
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>các bản ghi</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult GetAll();

        /// <summary>
        /// Lấy ra các bản ghi 
        /// </summary>
        /// <param name="id">id bản ghi cần lấy</param>
        /// <returns>Bản ghi có id</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult GetById(Guid id);

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult Insert(T entity);

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần cập nhật</param>
        /// <param name="entity">Số hàng bị ảnh hưởng</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult Update(Guid id, T entity);

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">id bảng ghi cần xóa</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult Delete(Guid id);

       /// <summary>
       /// Lấy bản ghi có cột và giá trị truyền vào
       /// </summary>
       /// <param name="name"></param>
       /// <param name="value"></param>
       /// <returns></returns>
       /// CreatedBy : LP(6/8)
        public ServiceResult GetByNameAndValueProperty(string name, string value);

        /// <summary>
        /// Dịch vụ kiểm tra trùng
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult CheckDuplicate(string columnName, string value, Guid id);
        #endregion
    }
}
