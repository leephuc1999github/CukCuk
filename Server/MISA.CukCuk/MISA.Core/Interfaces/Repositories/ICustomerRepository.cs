using MISA.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public IEnumerable<EmployeeCode> InsertMultiCustomer(string codes, int size, string delim);
    }
}
