using Dapper;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class ServiceAdditionRepository : BaseRepository<ServiceAddition>, IServiceAdditionRepository
    {
        #region Contructor
        public ServiceAdditionRepository()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách dịch vụ theo món ăn
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public IEnumerable<ServiceAddition> GetServiceAdditionsByInventoryItemId(Guid inventoryItemId)
        {
            try
            {
                string sqlCommand = "Proc_GetServiceAdditionsByInventoryItemId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("InventoryItemId", inventoryItemId.ToString());
                var serviceAdditions = _dbConnection.Query<ServiceAddition>(sqlCommand, parameters, commandType: CommandType.StoredProcedure);
                return serviceAdditions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception service additions {ex.Message}");
                return null;
            }
        }
        #endregion
    }
}
