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
    public class UnitService : BaseService<Unit>, IUnitService
    {
        #region Declare
        private readonly IUnitRepository _unitRepository;
        #endregion

        #region Contructor
        public UnitService(IBaseRepository<Unit> baseRepository, IUnitRepository unitRepository) : base(baseRepository)
        {
            this._unitRepository = unitRepository;
        }
        #endregion
    }
}
