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
    /// <summary>
    /// Dịch vụ chức vụ
    /// </summary>
    /// CreatedBy : LP(12/8)
    public class PositionService : BaseService<Position>, IPositionService
    {
        #region Constructor
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {

        }
        #endregion
    }
}
