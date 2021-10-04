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
    public class InventoryItemServiceAdditionService : BaseService<InventoryItemServiceAddition>, IInventoryItemServiceAdditionService
    {
        #region Declare
        private readonly IInventoryItemServiceAdditionRepository _inventoryItemServiceAdditionRepository;
        #endregion

        #region Contructor
        public InventoryItemServiceAdditionService(IBaseRepository<InventoryItemServiceAddition> baseRepository, IInventoryItemServiceAdditionRepository inventoryItemServiceAdditionRepository) : base(baseRepository)
        {
            this._inventoryItemServiceAdditionRepository = inventoryItemServiceAdditionRepository;
        }
        #endregion
    }
}
