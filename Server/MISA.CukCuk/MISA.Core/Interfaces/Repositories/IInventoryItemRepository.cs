using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IInventoryItemRepository : IBaseRepository<InventoryItem>
    {
        /// <summary>
        /// Phân trang và tìm kiếm món ăn
        /// </summary>
        /// <returns></returns>
        public BaseEntityPaging<InventoryItem> GetInventoryItemsPaging(List<BaseFilter> filters, int pageIndex, int pageSize);

        /// <summary>
        /// Thêm mới món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public int InsertInventoryItem(InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions);

        /// <summary>
        /// Cấp nhật món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public int UpdateInventoryItem(Guid inventoryItemId, InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions);
    }
}
