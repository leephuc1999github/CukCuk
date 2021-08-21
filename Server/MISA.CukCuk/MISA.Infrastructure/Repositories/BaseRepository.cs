using Dapper;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
    {
        #region Declare
        protected string _tableName;
        protected string _connectString;
        protected IDbConnection _dbConnection;
        #endregion

        #region Constructor
        public BaseRepository()
        {
            _tableName = typeof(T).Name;
            _connectString = "" +
                "Host = 47.241.69.179;" +
                "Database = MF948_LKPHUC_CukCuk;" +
                "User Id = dev;" +
                "Password = 12345678";
            _dbConnection = new MySqlConnection(_connectString);

        }
        #endregion

        #region Method
        /// <summary>
        /// PHân trang và tìm kiếm 
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm </param>
        /// <param name="pageIndex">Thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trên một trang</param>
        /// <returns></returns>
        public virtual BaseEntityPaging<T> GetPaging(string keyword, int pageIndex, int pageSize)
        {
            BaseEntityPaging<T> result = new BaseEntityPaging<T>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword);
                parameters.Add("@PageIndex", pageIndex);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                string sqlCommand = $"Proc_Get{_tableName}sPaging";

                var entities = _dbConnection.Query<T>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
                result.Data = entities;
                result.PageIndex = pageIndex;
                result.PageSize = pageSize;
                result.TotalRecord = parameters.Get<int>("TotalRecord"); 
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception GetPaging {_tableName} {ex.Message}");
                return result;
            }
            
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public virtual int Delete(Guid id)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add($"@{_tableName}Id", id.ToString());
                string sqlCommand = $"Proc_Delete{_tableName}ById";
                int rowEffects = _dbConnection.Execute(sqlCommand, param: parameter, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Delete {_tableName} {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                string sqlCommand = $"Proc_Get{_tableName}s";
                var entities = _dbConnection.Query<T>(sqlCommand, commandType: CommandType.StoredProcedure);
                return entities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception GetAll {_tableName} {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public virtual T GetById(Guid id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add($"@{_tableName}Id", id.ToString());
            string sqlCommand = $"Proc_Get{_tableName}ById";
            var entity = _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: parameter, commandType: CommandType.StoredProcedure);
            return entity;
        }

        /// <summary>
        /// Lấy bản ghi theo thuộc tính
        /// </summary>
        /// <param name="propertyName">Tên thuộc tính</param>
        /// <param name="propertyValue">Giá trị thuộc tính</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public virtual IEnumerable<T> GetByProperty(string propertyName, string propertyValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sqlCommand = $"Proc_Get{_tableName}By{propertyName}";
            parameters.Add("@Value", propertyValue);

            var entities = _dbConnection.Query<T>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
            return entities;

        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public virtual int Insert(T entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                string sqlCommand = $"Proc_Insert{_tableName}";
                foreach (PropertyInfo prop in entity.GetType().GetProperties())
                {
                    var value = prop.GetValue(entity) == "" ? null : prop.GetValue(entity);
                    parameters.Add($"@{prop.Name}", value);
                }
                _dbConnection.Open();
                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowAffects = _dbConnection.Execute(sqlCommand, param: parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowAffects;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Insert {_tableName} {ex.Message}");
                return 0;
            }

        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        public virtual int Update(Guid id, T entity)
        {
            try
            {

                DynamicParameters parameters = new DynamicParameters();

                string sqlCommand = $"Proc_Update{_tableName}";
                foreach (PropertyInfo prop in entity.GetType().GetProperties())
                {
                    var value = prop.GetValue(entity) == "" ? null : prop.GetValue(entity);
                    parameters.Add($"@{prop.Name}", value);
                }
                parameters.Add($"@{_tableName}Id", id.ToString());
                _dbConnection.Open();
                using (var transaction = _dbConnection.BeginTransaction())
                {
                    int rowAffects = _dbConnection.Execute(sqlCommand, param: parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return rowAffects;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Update {_tableName} {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Ngắt kết nối csdl
        /// </summary>
        /// CreatedBy : LP(12/8)
        public virtual void Dispose()
        {
            try
            {
                this._dbConnection.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Dispose {_tableName} {ex.Message}");
            }
        }

        /// <summary>
        /// KIểm tra trùng
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsDuplication(string name, string value, Guid id)
        {
            try
            {
                string sqlCommand = $"Proc_CheckDuplicate{name}";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Value", value);
                parameters.Add("@Id", id.ToString());
                var entities = _dbConnection.Query<T>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
                if (entities.Count() > 0) return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception CheckDuplication {_tableName} {ex.Message}");
            }
            return false;
        }
        #endregion
    }
}
