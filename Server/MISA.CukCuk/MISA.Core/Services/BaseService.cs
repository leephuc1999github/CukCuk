using MISA.Core.Enitites;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Property
        protected IBaseRepository<T> _baseRepository;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa một đối tượng
        /// </summary>
        /// <param name="id">id đối tượng cần xóa</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult Delete(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.DELETE;
            try
            {
                var entity = _baseRepository.GetById(id);
                if (entity != null)
                {
                    int rowEffects = _baseRepository.Delete(id);
                    if (rowEffects > 0)
                    {
                        serviceResult.SetSuccess(serviceResult, rowEffects);
                    }
                    else
                    {
                        serviceResult.SetBadRequest(serviceResult);
                        serviceResult.DevMessage.Add($"{id.ToString()} xóa không thành công");
                    }
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add($"{id.ToString()} không tồn tại");
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }


        /// <summary>
        /// Lấy tất cả bản ghi trong csdl
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var result = _baseRepository.GetAll();
                if (result.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, result);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }

            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }

        /// <summary>
        /// Lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">id đối tượng cần tìm</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult GetById(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var result = _baseRepository.GetById(id);

                if (result != null)
                {
                    serviceResult.SetSuccess(serviceResult, result);
                }
                else
                {
                    serviceResult.SetBadRequest(serviceResult);
                    serviceResult.DevMessage.Add($"Lấy mã {id.ToString()} không thành công");
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }


        /// <summary>
        /// CHèn một đối tượng 
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult Insert(T entity)
        {
            ServiceResult serviceResult = this.CheckValidate(new Guid(), entity);
            serviceResult.MoreInfo = Properties.Resource.POST;
            if (serviceResult.ResultCode == (int)EnumServiceResult.Success)
            {
                try
                {
                    int rowEffects = _baseRepository.Insert(entity);
                    if (rowEffects > 0)
                    {
                        serviceResult.SetSuccess(serviceResult, rowEffects);
                    }
                    else
                    {
                        serviceResult.SetNoContent(serviceResult);
                    }
                }
                catch (Exception ex)
                {
                    serviceResult.SetInternalServerError(serviceResult);
                    serviceResult.DevMessage.Add($"Exception {ex.Message}");
                }

            }
            return serviceResult;
        }

        /// <summary>
        /// Cập nhật một đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult Update(Guid id, T entity)
        {
            ServiceResult serviceResult = this.CheckValidate(id, entity);
            serviceResult.MoreInfo = Properties.Resource.PUT;
            try
            {
                var oldEntity = _baseRepository.GetById(id);
                if (oldEntity != null)
                {
                    // kiểm tra trường mã có trong csdl ?
                    int rowEffects = _baseRepository.Update(id, entity);
                    if (rowEffects > 0)
                    {
                        serviceResult.SetSuccess(serviceResult, rowEffects);
                    }
                    else
                    {
                        serviceResult.SetNoContent(serviceResult);
                    }
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                    serviceResult.DevMessage.Add($"Lấy mã {id.ToString()} không thành công");
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
        /// Kiểm tra đầu vào dữ liệu 
        /// </summary>
        /// <param name="entity">Thông tin đối tượng </param>
        /// <returns>Thông tin sau khi validate</returns>
        /// CreatedBy : LP(6/8)
        public ServiceResult CheckValidate(Guid id, T entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    // gán giá trị khóa
                    if(IsKey(property))
                    {
                        property.SetValue(entity, id);
                    }
                    // kiểm tra thuộc tính required
                    if (IsNull(property, entity))
                    {
                        serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                        serviceResult.DevMessage.Add(string.Format(Properties.Resource.Required_Msg, property.Name));
                        serviceResult.UserMessage = string.Format(Properties.Resource.Required_Msg, property.Name);
                    }
                    // kiểm tra giá trị  trùng
                    if (property.IsDefined(typeof(Duplication), false))
                    {
                        string name = property.Name;
                        var value = property.GetValue(entity);
                        if (_baseRepository.IsDuplication(property.Name, (string)value, id)){
                            serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                            serviceResult.DevMessage.Add(string.Format(Properties.Resource.Duplicate_Msg, name));
                            serviceResult.UserMessage = string.Format(Properties.Resource.Duplicate_Msg, name);
                        }
                        
                    }
                    // kiểm tra email
                    if(IsNotEmail(property, entity))
                    {
                        serviceResult.ResultCode = (int)EnumServiceResult.BadRequest;
                        serviceResult.DevMessage.Add(string.Format(Properties.Resource.Email_Msg, property.Name));
                        serviceResult.UserMessage = string.Format(Properties.Resource.Email_Msg, property.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }

        /// <summary>
        /// Thuộc tính là khóa
        /// </summary>
        /// <param name="property">Thuộc tính</param>
        /// <returns></returns>
        private bool IsKey(PropertyInfo property)
        {
            return property.IsDefined(typeof(Key), true);
        }

        /// <summary>
        /// Kiểm tra giá trị null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNull(PropertyInfo property, T entity)
        {
            var value = property.GetValue(entity);
            if(property.IsDefined(typeof(Required), true))
            {
                if (string.IsNullOrEmpty(Convert.ToString(value)) || value == null) return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra trùng với giá trị có trước
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //private bool IsDuplication(string name, string value)
        //{
        //    var entityDuplicate = _baseRepository.GetByProperty(name, value);
        //    if (entityDuplicate != null) return true;
        //    return false;
        //}

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNotEmail(PropertyInfo property, T entity)
        {
            var email = property.GetValue(entity);
            if(property.IsDefined(typeof(Email), true))
            {
                string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
                Regex regex = new Regex(pattern);

                if (regex.Matches((string)email).Count == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Lấy đối tượng theo cặp dữ liệu : tên cột và giá trị
        /// </summary>
        /// <param name="name">Tên cột</param>
        /// <param name="value">Giá trị cột</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult GetByNameAndValueProperty(string name, string value)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.GET;
            try
            {
                var entity = _baseRepository.GetByProperty(name, value);
                if (entity != null)
                {
                    serviceResult.SetSuccess(serviceResult, entity);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.SetInternalServerError(serviceResult);
                serviceResult.DevMessage.Add($"Exception {ex.Message}");
            }
            return serviceResult;
        }
        #endregion
    }
}
