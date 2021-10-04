using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IServiceAdditionRepository : IBaseRepository<ServiceAddition>
    {
        /// <summary>
        /// Lấy danh sách dịch vụ theo món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<ServiceAddition> GetServiceAdditionsByInventoryItemId(Guid inventoryItemId);
    }
}
