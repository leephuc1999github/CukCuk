using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoryItemServiceAdditionsController : BaseController<InventoryItemServiceAddition>
    {
        #region Declare
        private readonly IInventoryItemServiceAdditionService _inventoryItemServiceAdditionService;
        #endregion

        #region Constructor
        public InventoryItemServiceAdditionsController(IBaseService<InventoryItemServiceAddition> baseService, IInventoryItemServiceAdditionService inventoryItemServiceAdditionService) : base(baseService)
        {
            this._inventoryItemServiceAdditionService = inventoryItemServiceAdditionService;
        }
        #endregion
    }
}
