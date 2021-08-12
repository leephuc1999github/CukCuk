using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;

namespace MISA.Infrastructure.Repositories
{
    /// <summary>
    /// Triểu khai với nhân viên 
    /// </summary>
    /// CreatedBy : LP(6/8)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository()
        {

        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public string GetNewEmployeeCode()
        {
            string oldCode = _dbConnection.QueryFirstOrDefault<string>("Proc_GetNewEmployeeCode", commandType: CommandType.StoredProcedure);
            string newCode = GenerateEmployeeCode(oldCode);
            return newCode;
        }

        /// <summary>
        /// Tạo mã mới
        /// </summary>
        /// <param name="oldCode">Mã nhân viên cũ</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        private string GenerateEmployeeCode(string oldCode)
        {
            string changeValue = "";
            int plus = 1;
            int brk = 0;
            for (int i = oldCode.Length - 1; i >= 0; i--)
            {
                brk = i;
                if (oldCode[i] >= '0' && oldCode[i] <= '9')
                {
                    int n = (int)(oldCode[i] - '0');
                    if (n + plus >= 10)
                    {
                        int v = (n + plus) - 10;
                        plus = 1;
                        changeValue = v.ToString() + changeValue;
                    }
                    else
                    {
                        changeValue = (n + plus).ToString() + changeValue;
                        break;
                    }
                }
                else
                {
                    brk += 1;
                    changeValue = plus.ToString() + changeValue;
                    break;
                }
            }
            return oldCode.Substring(0, brk) + changeValue;
        }

    }
}
