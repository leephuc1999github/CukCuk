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
    public class PictureService : BaseService<Picture>, IPictureService
    {
        #region Declare
        private readonly IPictureRepository _inventoryItemRepository;
        #endregion

        #region Contructor
        public PictureService(IBaseRepository<Picture> baseRepository, IPictureRepository inventoryItemRepository) : base(baseRepository)
        {
            this._inventoryItemRepository = inventoryItemRepository;
        }
        #endregion
    }
}
