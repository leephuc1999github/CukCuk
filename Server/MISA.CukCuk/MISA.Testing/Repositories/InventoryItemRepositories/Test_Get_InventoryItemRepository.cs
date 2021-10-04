using Microsoft.VisualStudio.TestTools.UnitTesting;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using MISA.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.Testing.Repositories
{
    [TestClass]
    public class Test_Get_InventoryItemRepository
    {
        private IInventoryItemRepository _inventoryItemRepository;
        [TestInitialize]
        public void Initialize()
        {
            _inventoryItemRepository = new InventoryItemRepository();
        }

        /// <summary>
        /// Case lấy ds món ăn phân trang : không có điều kiện 
        /// </summary>
        [TestMethod]
        public void GetInventoryItemsPaging_NoCondition()
        {
            List<BaseFilter> filters = new List<BaseFilter>();
            int pageSize = -100;
            int pageIndex = -100;
            var actual = _inventoryItemRepository.GetInventoryItemsPaging(filters, pageIndex, pageSize);
            // Bảng inventoryitem : 46 bản ghi

            Assert.AreEqual(actual.PageIndex, 1);
            Assert.AreEqual(actual.PageSize, 25);
            Assert.AreEqual(actual.TotalPage, 2);
            Assert.AreEqual(actual.TotalRecord, 46);
            Assert.AreEqual(actual.Data.Count(), 25);
        }

        /// <summary>
        /// Case lấy ds món ăn phân trang : có dữ liệu
        /// </summary>
        [TestMethod]
        public void GetInventoryItemPaging_WithCondition()
        {
            List<BaseFilter> filters = new List<BaseFilter>()
            {
                new BaseFilter() { KeySearch = null, NameProperty = "IdInventoryItem", Operator = "like" },
                new BaseFilter() { KeySearch = "'%Canh%'", NameProperty = "CodeInventoryItem", Operator = "like" },
                new BaseFilter() { KeySearch = "'%Cá%'", NameProperty = "NameInventoryItem", Operator = "abc" },
                new BaseFilter() { KeySearch = "'Món ăn'", NameProperty = "TypeInventoryItem", Operator = "=" },
                new BaseFilter() { KeySearch = null, NameProperty = "NameInventoryItemGroup_FULLTEXT", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "NameUnit_FULLTEXT", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "Price", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "OriginalPrice", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "DescriptionInventoryItem", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "UnitId", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "InventoryItemGroupId", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "ProcessingIn", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "Active", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "PictureId", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "IsChangeByTime", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "IsAutoChange", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "HasMaterial", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "StoppedSell", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "UrlPicture", Operator = "like" },

            };
            int pageSize = 25;
            int pageIndex = 1;
            var actual = _inventoryItemRepository.GetInventoryItemsPaging(filters, pageIndex, pageSize);
            // Bảng inventoryitem : 46 bản ghi

            Assert.AreEqual(actual.PageIndex, 1);
            Assert.AreEqual(actual.PageSize, 25);
            Assert.AreEqual(actual.TotalPage, 1);
            Assert.AreEqual(actual.TotalRecord, 4);
            Assert.AreEqual(actual.Data.Count(), 4);
        }


        /// <summary>
        /// Case lấy ds món ăn phân trang : có dữ liệu
        /// </summary>
        [TestMethod]
        public void GetInventoryItemPaging_WithCondition1()
        {
            List<BaseFilter> filters = new List<BaseFilter>()
            {
                new BaseFilter() { KeySearch = null, NameProperty = "CodeInventoryItem", Operator = "like" },
                new BaseFilter() { KeySearch = "'%Cá%'", NameProperty = "NameInventoryItem", Operator = "abc" },
                new BaseFilter() { KeySearch = "'Món ăn'", NameProperty = "TypeInventoryItem", Operator = "=" },
                new BaseFilter() { KeySearch = null, NameProperty = "NameInventoryItemGroup_FULLTEXT", Operator = "like" },
                new BaseFilter() { KeySearch = "'%cái%'", NameProperty = "NameUnit_FULLTEXT", Operator = "like" },
                new BaseFilter() { KeySearch = "10000", NameProperty = "PriceDEMO", Operator = ">" },
                new BaseFilter() { KeySearch = null, NameProperty = "OriginalPrice", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "DescriptionInventoryItem", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "UnitId", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "InventoryItemGroupId", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "ProcessingIn", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "Active", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "PictureId", Operator = "like" },
                new BaseFilter() { KeySearch = "0", NameProperty = "IsChangeByTime", Operator = "=" },
                new BaseFilter() { KeySearch = null, NameProperty = "IsAutoChange", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "HasMaterial", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "StoppedSell", Operator = "like" },
                new BaseFilter() { KeySearch = null, NameProperty = "UrlPicture", Operator = "like" },

            };
            int pageSize = 25;
            int pageIndex = 1;
            var actual = _inventoryItemRepository.GetInventoryItemsPaging(filters, pageIndex, pageSize);
            // Bảng inventoryitem : 46 bản ghi

            Assert.AreEqual(actual.PageIndex, 1);
            Assert.AreEqual(actual.PageSize, 25);
            Assert.AreEqual(actual.TotalPage, 1);
            Assert.AreEqual(actual.TotalRecord, 3);
            Assert.AreEqual(actual.Data.Count(), 3);
        }

    }
}
