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
    public class InventoryItemGroupService : BaseService<InventoryItemGroup>, IInventoryItemGroupService
    {
        #region Declare
        private readonly IInventoryItemGroupRepository _inventoryItemGroupRepository;
        #endregion

        #region Contructor
        public InventoryItemGroupService(IBaseRepository<InventoryItemGroup> baseRepository, IInventoryItemGroupRepository inventoryItemGroupRepository) : base(baseRepository)
        {
            this._inventoryItemGroupRepository = inventoryItemGroupRepository;
        }
        #endregion
    }
}
