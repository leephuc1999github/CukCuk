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
    public class ServiceAdditionService : BaseService<ServiceAddition>, IServiceAdditionService
    {
        #region Declare
        private readonly IServiceAdditionRepository _serviceAdditionRepository;
        #endregion

        #region Contructor
        public ServiceAdditionService(IBaseRepository<ServiceAddition> baseRepository, IServiceAdditionRepository serviceAdditionRepository) : base(baseRepository)
        {
            this._serviceAdditionRepository = serviceAdditionRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dịch vụ láy dịch vụ theo món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public ServiceResult GetServiceAdditionsByInventoryItemId(Guid inventoryItemId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                var serviceAdditions = this._serviceAdditionRepository.GetServiceAdditionsByInventoryItemId(inventoryItemId);
                if(serviceAdditions.Count() > 0)
                {
                    serviceResult.SetSuccess(serviceResult, serviceAdditions);
                }
                else
                {
                    serviceResult.SetNoContent(serviceResult);
                }
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.DevMessage.Add(ex.Message);
                serviceResult.SetInternalServerError(serviceResult);
                return serviceResult;
            }
        }
        #endregion
    }
}
