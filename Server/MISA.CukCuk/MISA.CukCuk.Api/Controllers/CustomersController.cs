
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseController<Customer>
    {

        #region Declare
        private readonly ICustomerService _customerService;
        #endregion
        #region Constructor
        public CustomersController(IBaseService<Customer> baseService, ICustomerService customerService) : base(baseService)
        {
            this._customerService = customerService;
        }
        #endregion

        #region Method

        [HttpPost]
        [Route("Import")]
        public IActionResult ImportCustomer(IFormFile formFile)
        {
            var resultService = _customerService.ImportCustomer(formFile); 
            return Ok(resultService);
        }


        [HttpGet]
        [Route("Export")]
        public IActionResult ExportCustomer()
        {
            var resultService = _customerService.ExportCustomer();
            if(resultService.Data != null)
            {
                Stream stream = (Stream)resultService.Data;
                stream.Position = 0;
                string excelName = $"Customers-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            return Ok("Thực hiện khoogn thành công");
        }
        #endregion
    }
}
