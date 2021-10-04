using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Declare
        private readonly IBaseService<T> _baseService;
        #endregion

        #region Contructor
        public BaseController(IBaseService<T> baseService)
        {
            this._baseService = baseService;
        }
        #endregion

        #region Method
        [HttpGet]
        public IActionResult GetAll()
        {
            var serviceResult = _baseService.GetAll();
            return Ok(serviceResult);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var serviceResult = _baseService.GetById(id);
            return Ok(serviceResult);
        }


        [HttpGet]
        [Route("Property")]
        public IActionResult GetByProperty(string name, string value)
        {
            var serviceResult = _baseService.GetByNameAndValueProperty(name, value);
            return Ok(serviceResult);
        }

        [HttpPost]
        public virtual IActionResult Insert(T entity)
        {
            var resultService = _baseService.Insert(entity);
            return Ok(resultService);
        }
        

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, T entity)
        {
            var serviceResult = _baseService.Update(id, entity);
            return Ok(serviceResult);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var serviceResult = _baseService.Delete(id);
            return Ok(serviceResult);
        }

        [HttpGet("Duplicate")]
        public IActionResult CheckDuplicate(string columnName, string value, Guid id)
        {
            var serviceResult = this._baseService.CheckDuplicate(columnName, value, id);
            return Ok(serviceResult);
        }
        #endregion
    }
}
