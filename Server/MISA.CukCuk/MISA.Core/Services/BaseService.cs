using MISA.Core.Enitites;
using MISA.Core.Enumerations;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                int rowEffects = _baseRepository.Delete(id);
                if(rowEffects > 0)
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
                if(result.Count() > 0)
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

                if(result != null)
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
        /// CHèn một đối tượng 
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public ServiceResult Insert(T entity)
        {
            ServiceResult serviceResult = CheckValidate(entity);
            serviceResult.MoreInfo = Properties.Resource.POST;
            if(serviceResult.ResultCode == (int)EnumServiceResult.Success)
            {
                try
                {
                    int rowEffects = _baseRepository.Insert(entity);
                    if(rowEffects > 0)
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
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.MoreInfo = Properties.Resource.PUT;
            try
            {
                int rowEffects = _baseRepository.Update(id, entity);
                if(rowEffects > 0)
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
        private ServiceResult CheckValidate(T entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                foreach (var property in entity.GetType().GetProperties())
                {
                    var name = property.Name;
                    var value = property.GetValue(entity) == "" ? null : property.GetValue(entity);
                    if (property.IsDefined(typeof(Required), false) && IsNull(value))
                    {
                        serviceResult.ResultCode = (int)EnumServiceResult.Forbidden;
                        serviceResult.DevMessage.Add($"Giá trị {name} không được để trống");
                        serviceResult.UserMessage = $"Giá trị {name} không được để trống";
                    }

                    if(property.IsDefined(typeof(Duplication), false) && IsDuplication(name, (string)value))
                    {
                        serviceResult.ResultCode = (int)EnumServiceResult.Forbidden;
                        serviceResult.DevMessage.Add($"Giá trị {name} đã tồn tại");
                        serviceResult.UserMessage = $"Giá trị {name} đã tồn tại";
                    }

                    if(property.IsDefined(typeof(Email), false))
                    {
                        if (IsNotEmail(value.ToString().Trim()))
                        {
                            serviceResult.ResultCode = (int)EnumServiceResult.Forbidden;
                            serviceResult.DevMessage.Add($"Giá trị {name} không đúng định dạng");
                            serviceResult.UserMessage = $"Giá trị {name} không đúng định dạng";
                        }
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
        /// Kiểm tra giá trị null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNull(object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)) || value == null) return true;
            return false;
        }

        /// <summary>
        /// Kiểm tra trùng với giá trị có trước
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsDuplication(string name, string value)
        {
            var entityDuplicate = _baseRepository.GetByProperty(name, value);
            if (entityDuplicate != null) return true;
            return false;
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNotEmail(string email) 
        {
            string pattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;,.]{0,1}\s*)+$";
            Regex regex = new Regex(pattern);

            if (regex.Matches(email).Count == 0)
                return true;
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
                if(entity != null)
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
