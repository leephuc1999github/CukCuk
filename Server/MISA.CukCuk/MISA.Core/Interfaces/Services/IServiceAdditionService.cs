using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IServiceAdditionService : IBaseService<ServiceAddition>
    {
        /// <summary>
        /// DỊch vụ lấy ds dịch vụ theo món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public ServiceResult GetServiceAdditionsByInventoryItemId(Guid inventoryItemId);
    }
}
