using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IInventoryItemService : IBaseService<InventoryItem>
    {
        /// <summary>
        /// Dịch vụ phân trang tìm kiếm 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ServiceResult GetInventoryItemsPaging(List<BaseFilter> filters, int pageIndex, int pageSize);

        /// <summary>
        /// Thêm mới món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public ServiceResult InsertInventoryItem(InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions);

        /// <summary>
        /// Dịch vụ sửa món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public ServiceResult UpdateInventoryItem(Guid inventoryItemId, InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions);

        /// <summary>
        /// Dịch vụ sinh mã mới
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult NewInventoryItemCode(string columnName, string value, Guid id);
    }
}
