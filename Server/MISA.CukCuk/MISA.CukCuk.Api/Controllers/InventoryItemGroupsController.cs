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
    public class InventoryItemGroupsController : BaseController<InventoryItemGroup>
    {
        #region Declare
        private readonly IInventoryItemGroupService _inventoryItemGroupService;
        #endregion

        #region Constructor
        public InventoryItemGroupsController(IBaseService<InventoryItemGroup> baseService, IInventoryItemGroupService inventoryItemGroupService) : base(baseService)
        {
            this._inventoryItemGroupService = inventoryItemGroupService;
        }
        #endregion
    }
}
