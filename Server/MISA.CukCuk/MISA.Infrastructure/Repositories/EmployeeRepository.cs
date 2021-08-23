using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="posistionId"></param>
        /// <param name="departmentId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public BaseEntityPaging<Employee> GetEmployeesPaging(string keyword, string positionId, string departmentId, int pageIndex, int pageSize)
        {
            BaseEntityPaging<Employee> result = new BaseEntityPaging<Employee>();

            string sqlCommand = "Proc_GetEmployeesPaging";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("@PositionId", positionId);
            parameters.Add("@DepartmentId", departmentId);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var employees = _dbConnection.Query<Employee>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
            result.TotalRecord = parameters.Get<int>("TotalRecord");


            result.Data = employees;
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;

            return result;
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

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteMultiEmployees(List<Guid> ids)
        {
            bool flag = true;
            try
            {
                _dbConnection.Open();
                using (var transaction = _dbConnection.BeginTransaction())
                {
                    // duyệt từng id
                    foreach (var item in ids)
                    {
                        //xóa 
                        
                        string sqlCommand = $"DELETE FROM Employee WHERE EmployeeId = '{item.ToString()}'";
                        int rowEffects = _dbConnection.Execute(sqlCommand, transaction: transaction, commandType: CommandType.Text);
                        if (rowEffects == 0)
                        {
                            // xóa không thành công
                            flag = false;
                            transaction.Rollback();
                            break;
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return flag;
        }

    }
}
