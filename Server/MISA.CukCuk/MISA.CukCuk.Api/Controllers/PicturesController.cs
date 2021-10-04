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
    public class PicturesController : BaseController<Picture>
    {
        #region Declare
        private readonly IPictureService _pictureService;
        #endregion

        #region Constructor
        public PicturesController(IBaseService<Picture> baseService, IPictureService pictureService) : base(baseService)
        {
            this._pictureService = pictureService;
        }
        #endregion
    }
}
