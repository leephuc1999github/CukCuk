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
    public class ServiceAdditionsController : BaseController<ServiceAddition>
    {
        #region Declare
        private readonly IServiceAdditionService _serviceAdditionService;
        #endregion

        #region Constructor
        public ServiceAdditionsController(IBaseService<ServiceAddition> baseService, IServiceAdditionService serviceAdditionService) : base(baseService)
        {
            this._serviceAdditionService = serviceAdditionService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("Filter")]
        public IActionResult GetServiceAdditions(Guid inventoryItemId)
        {
            var serviceAdditions = this._serviceAdditionService.GetServiceAdditionsByInventoryItemId(inventoryItemId);
            return Ok(serviceAdditions);
        }
        #endregion
    }
}
