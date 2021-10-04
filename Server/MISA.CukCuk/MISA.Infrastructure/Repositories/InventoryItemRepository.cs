using Dapper;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
    {
        #region Contructor
        public InventoryItemRepository()
        {
        }


        #endregion

        #region Methods
        /// <summary>
        /// Phân trang tìm kiếm món ăn
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public BaseEntityPaging<InventoryItem> GetInventoryItemsPaging(List<BaseFilter> filters, int pageIndex, int pageSize)
        {
            BaseEntityPaging<InventoryItem> result = new BaseEntityPaging<InventoryItem>();
            try
            {
                // Kiểm tra thông tin trang và số bảng ghi / trang
                pageIndex = pageIndex < 0 ? 1 : pageIndex;
                pageSize = pageSize < 0 ? 25 : pageSize;
                // Tham số store
                DynamicParameters parameters = new DynamicParameters();
                string sqlColumn = "IdInventoryItem, NameInventoryItem, Price, Active, " +
                    $"CodeInventoryItem, NameUnit_FULLTEXT, NameInventoryItemGroup_FULLTEXT, IsChangeByTime, " +
                    $"TypeInventoryItem, IsAutoChange, HasMaterial, StoppedSell";

                // Lấy thuộc tính của món ăn
                HashSet<string> nameProperties = new HashSet<string>();
                InventoryItem inventoryItem = new InventoryItem();
                foreach (PropertyInfo property in inventoryItem.GetType().GetProperties())
                {
                    nameProperties.Add(property.Name);
                }
                // Lấy những tên cột filter tồn tại trong ds tên thuộc tính
                string sqlWhere = "";
                string[] operators = { "like", "=", "not like", ">", ">=", "<", "<=", "=", "between", "in", "<>" };
                int counter = 0;
                foreach (var filterItem in filters)
                {
                    if (nameProperties.Contains(filterItem.NameProperty) &&
                        operators.Contains(filterItem.Operator.ToLower()) &&
                        filterItem.KeySearch != null)
                    {
                        if (counter == 0) sqlWhere += $"{filterItem.NameProperty} {filterItem.Operator} {filterItem.KeySearch} ";
                        else sqlWhere += $"and {filterItem.NameProperty} {filterItem.Operator} {filterItem.KeySearch} ";
                        counter++;
                    }
                }


                // Gán tham số vào store
                parameters.Add("@sqlWhere", sqlWhere);
                parameters.Add("@sqlColumn", sqlColumn);
                parameters.Add("@pageIndex", pageIndex);
                parameters.Add("@pageSize", pageSize);
                parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                string sqlCommand = $"Proc_GetInventoryItemsPaging";
                // Lấy ds phân trang
                var entities = _dbConnection.Query<InventoryItem>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);
                // Trả về kết quả
                result.Data = entities;
                result.PageIndex = pageIndex;
                result.PageSize = pageSize;
                result.TotalRecord = parameters.Get<int>("TotalRecord");
                result.TotalPage = parameters.Get<int>("TotalPage");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception GetPaging InventoryItem {ex.Message}");
                return result;
            }
        }

        /// <summary>
        /// Thêm mới món ăn
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public int InsertInventoryItem(InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions)
        {
            try
            {
                // Tham số 
                DynamicParameters inventoryItemParameters = new DynamicParameters();
                DynamicParameters pictureParameters = new DynamicParameters();
                // Tên procedure
                string insertInventoryItemCommand = "Proc_InsertInventoryItem";
                string insertPictureCommand = "Proc_InsertPicture";
                string insertServiceAdditionCommnad = "Proc_InsertInventoryItemServiceAddition";
                // Truyền tham số cho store
                inventoryItemParameters.Add("@NameInventoryItem", inventoryItem.NameInventoryItem);
                inventoryItemParameters.Add("@CodeInventoryItem", inventoryItem.CodeInventoryItem);
                inventoryItemParameters.Add("@InventoryItemGroupId", inventoryItem.InventoryItemGroupId);
                inventoryItemParameters.Add("@UnitId", inventoryItem.UnitId);
                inventoryItemParameters.Add("@Price", inventoryItem.Price);
                inventoryItemParameters.Add("@OriginalPrice", inventoryItem.OriginalPrice);
                inventoryItemParameters.Add("@DescriptionInventoryItem", inventoryItem.DescriptionInventoryItem);
                inventoryItemParameters.Add("@ProcessingIn", inventoryItem.ProcessingIn);
                inventoryItemParameters.Add("@PictureId", inventoryItem.PictureId);
                inventoryItemParameters.Add("@Active", inventoryItem.Active);
                inventoryItemParameters.Add("@IdInventoryItem", dbType: DbType.Guid, direction: ParameterDirection.Output);

                if (picture.IdPicture != new Guid())
                {
                    pictureParameters.Add("@IdPicture", picture.IdPicture);
                    pictureParameters.Add("@NamePicture", picture.NamePicture);
                    pictureParameters.Add("@TailPicture", picture.TailPicture);
                    pictureParameters.Add("@DescriptionPicture", picture.DescriptionPicture);
                    pictureParameters.Add("@DictionaryPicture", picture.DictionaryPicture);
                }


                // Kết nối
                _dbConnection.Open();
                // Executing
                using (var transaction = _dbConnection.BeginTransaction())
                {
                    // Thêm ảnh
                    if (picture.IdPicture != new Guid())
                    {
                        int insertPicture = _dbConnection.Execute(insertPictureCommand, param: pictureParameters, transaction, commandType: CommandType.StoredProcedure);
                        if (insertPicture == 0) return 0;
                    }
                    // Thêm món ăn
                    int insertInventoryItem = _dbConnection.Execute(insertInventoryItemCommand, param: inventoryItemParameters, transaction, commandType: CommandType.StoredProcedure);
                    // Thêm mới món ăn không thành công
                    if (insertInventoryItem == 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                    // Lấy id món ăn vừa thêm
                    inventoryItem.IdInventoryItem = inventoryItemParameters.Get<Guid>("IdInventoryItem");
                    // Thêm mới danh sách dịch vụ thêm
                    foreach (var item in serviceAdditions)
                    {
                        // Nếu id != null => thêm mới
                        if (item.IdServiceAddition != new Guid())
                        {
                            DynamicParameters parameter = new DynamicParameters();
                            parameter.Add("@InventoryItemId", inventoryItem.IdInventoryItem);
                            parameter.Add("@ServiceAdditionId", item.IdServiceAddition);
                            parameter.Add("@PriceServiceAddition", item.PriceServiceAddition);

                            int execute = _dbConnection.Execute(insertServiceAdditionCommnad, param: parameter, transaction, commandType: CommandType.StoredProcedure);
                            if (execute == 0)
                            {
                                transaction.Rollback();
                                return 0;
                            }
                        }
                    }
                    transaction.Commit();
                    return 1;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Cập nhật món ăn theo id
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="inventoryItem"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        public int UpdateInventoryItem(Guid inventoryItemId, InventoryItem inventoryItem, Picture picture, List<ServiceAddition> serviceAdditions)
        {
            try
            {
                DynamicParameters inventoryItemParameters = new DynamicParameters();
                DynamicParameters pictureParameters = new DynamicParameters();
                string updateInventoryItemCommand = "Proc_UpdateInventoryItem";
                string updatePictureCommmand = "Proc_UpdatePicture";
                string updateServiceAdditionCommand = "Proc_UpdateInventoryItemServiceAddition";
                // Truyền tham số cho store
                inventoryItemParameters.Add("@NameInventoryItem", inventoryItem.NameInventoryItem);
                inventoryItemParameters.Add("@CodeInventoryItem", inventoryItem.CodeInventoryItem);
                inventoryItemParameters.Add("@InventoryItemGroupId", inventoryItem.InventoryItemGroupId);
                inventoryItemParameters.Add("@UnitId", inventoryItem.UnitId);
                inventoryItemParameters.Add("@Price", inventoryItem.Price);
                inventoryItemParameters.Add("@OriginalPrice", inventoryItem.OriginalPrice);
                inventoryItemParameters.Add("@DescriptionInventoryItem", inventoryItem.DescriptionInventoryItem);
                inventoryItemParameters.Add("@ProcessingIn", inventoryItem.ProcessingIn);
                inventoryItemParameters.Add("@PictureId", inventoryItem.PictureId);
                inventoryItemParameters.Add("@Active", inventoryItem.Active);
                inventoryItemParameters.Add("@InventoryItemId", inventoryItemId.ToString());

                if (picture.IdPicture != new Guid())
                {
                    pictureParameters.Add("@IdPicture", picture.IdPicture);
                    pictureParameters.Add("@NamePicture", picture.NamePicture);
                    pictureParameters.Add("@TailPicture", picture.TailPicture);
                    pictureParameters.Add("@DescriptionPicture", picture.DescriptionPicture);
                    pictureParameters.Add("@DictionaryPicture", picture.DictionaryPicture);
                }

                // Kết nối
                _dbConnection.Open();
                // Executing
                using (var transaction = _dbConnection.BeginTransaction())
                {
                    // Cập nhật ảnh
                    if (picture.IdPicture != new Guid())
                    {
                        int updatePicture = _dbConnection.Execute(updatePictureCommmand, param: pictureParameters, transaction, commandType: CommandType.StoredProcedure);
                        if (updatePicture == 0) return 0;
                    }
                    // Cập nhật ảnh
                    int updateInventoryItem = _dbConnection.Execute(updateInventoryItemCommand, param: inventoryItemParameters, transaction, commandType: CommandType.StoredProcedure);
                    if (updateInventoryItem == 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                    // Thêm dịch vụ món ăn
                    foreach (var item in serviceAdditions)
                    {
                        if (item.IdServiceAddition != new Guid())
                        {
                            DynamicParameters parameter = new DynamicParameters();
                            parameter.Add("@InventoryItemId", inventoryItem.IdInventoryItem);
                            parameter.Add("@ServiceAdditionId", item.IdServiceAddition);
                            parameter.Add("@PriceServiceAddition", item.PriceServiceAddition);

                            int execute = _dbConnection.Execute(updateServiceAdditionCommand, param: parameter, transaction, commandType: CommandType.StoredProcedure);
                            if (execute == 0)
                            {
                                transaction.Rollback();
                                return 0;
                            }
                        }
                    }

                    transaction.Commit();
                    return 1;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion
    }
}
