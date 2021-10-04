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
    public class UnitsController : BaseController<Unit>
    {
        #region Declare
        private readonly IUnitService _unitService;
        #endregion

        #region Constructor
        public UnitsController(IBaseService<Unit> baseService, IUnitService unitService) : base(baseService)
        {
            this._unitService = unitService;
        }
        #endregion
    }
}
