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
    public class InventoryItemService : BaseService<InventoryItem>, IInventoryItemService
    {
        #region Declare
        private readonly IInventoryItemRepository _inventoryItemRepository;
        #endregion

        #region Contructor
        public InventoryItemService(IBaseRepository<InventoryItem> baseRepository, IInventoryItemRepository inventoryItemRepository) : base(baseRepository)
        {
            this._inventoryItemRepository = inventoryItemRepository;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Dịch vụ phân trang tìm kiếm món ăn
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ServiceResult GetInventoryItemsPaging(List<BaseFilter> filters, int pageIndex, int pageSize)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                var inventoryItems = this._inventoryItemRepository.GetInventoryItemsPaging(filters, pageIndex, pageSize);
                if (inventoryItems.Data.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, inventoryItems);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
                return serviceResult;
            }
            return serviceResult;

        }

        /// <summary>
        /// Dịch vụ thêm mới món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public ServiceResult InsertInventoryItem(InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions)
        {
            ServiceResult serviceResult = this.CheckValidate(new Guid(), inventoryItem);
            try
            {
                if(serviceResult.ResultCode == 200)
                {
                    int result = this._inventoryItemRepository.InsertInventoryItem(inventoryItem, picture, serviceAdditions);
                    if (result == 1)
                    {
                        serviceResult.SetSuccess(serviceResult, result);
                    }
                    else
                    {
                        serviceResult.DevMessage.Add(Properties.Resource.User_Info_Msg);
                        serviceResult.UserMessage = Properties.Resource.User_Info_Msg;
                        serviceResult.SetNoContent(serviceResult);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }

        /// <summary>
        /// Dịch vụ sinh mã mới
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult NewInventoryItemCode(string columnName, string value, Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                bool test = this._baseRepository.IsDuplication(columnName, value, id);
                if (test)
                {
                    string code = value;
                    for(int counter = 1; ; counter++)
                    {
                        code = value + counter;
                        test = this._baseRepository.IsDuplication(columnName, value + counter, id);
                        if (!test)
                        {
                            break;
                        }
                    }
                    serviceResult.SetSuccess(serviceResult, code);
                }
                else
                {
                    serviceResult.SetSuccess(serviceResult, value);
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }

        /// <summary>
        /// DỊch vụ cập nhật món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public ServiceResult UpdateInventoryItem(Guid inventoryItemId, InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions)
        {
            ServiceResult serviceResult = this.CheckValidate(inventoryItemId, inventoryItem);
            try
            {
                if(serviceResult.ResultCode == 200)
                {
                    var isValid = this._baseRepository.GetById(inventoryItemId);
                    if(isValid != null)
                    {
                        int rowEffects = this._inventoryItemRepository.UpdateInventoryItem(inventoryItemId, inventoryItem, picture, serviceAdditions);
                        if(rowEffects > 0)
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
                        serviceResult.DevMessage.Add($"Lấy mã {inventoryItemId.ToString()} không thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
            }
            return serviceResult;
        }
        #endregion
    }
}
