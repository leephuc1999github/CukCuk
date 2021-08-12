using Dapper;
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
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public int Delete(Guid id)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add($"@{_tableName}Id", id.ToString());
            string sqlCommand = $"Proc_Delete{_tableName}ById";
            int rowEffects = _dbConnection.Execute(sqlCommand, param: parameter, commandType: CommandType.StoredProcedure);
            return rowEffects;
        }

        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public IEnumerable<T> GetAll()
        {
            string sqlCommand = $"Proc_Get{_tableName}s";
            var entities = _dbConnection.Query<T>(sqlCommand, commandType: CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// Lấy một bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public T GetById(Guid id)
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
        public T GetByProperty(string propertyName, string propertyValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sqlCommand = $"Proc_Get{_tableName}By{propertyName}";
            parameters.Add("@Value", propertyValue);

            var entity = _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
            return entity;

        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        /// CreatedBy : LP(12/8)
        public int Insert(T entity)
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

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns></returns>
        public int Update(Guid id, T entity)
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

        /// <summary>
        /// Ngắt kết nối csdl
        /// </summary>
        /// CreatedBy : LP(12/8)
        public void Dispose()
        {
            this._dbConnection.Dispose();
        }
        #endregion
    }
}
