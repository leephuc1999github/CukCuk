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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public IEnumerable<EmployeeCode> InsertMultiCustomer(string codes, int size, string delim)
        {
            try
            {
                string sqlCommand = "Proc_CheckDuplicateMultiEmployeeCode";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Codes", codes);
                parameters.Add("@Size", size);

                var customers = _dbConnection.Query<EmployeeCode>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
                return customers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }

    
}
