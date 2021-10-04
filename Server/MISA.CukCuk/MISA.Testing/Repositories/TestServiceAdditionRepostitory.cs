using Microsoft.VisualStudio.TestTools.UnitTesting;
using MISA.Core.Interfaces.Repositories;
using MISA.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Testing.Repositories
{
    [TestClass]
    public class TestServiceAdditionRepository
    {
        private IServiceAdditionRepository _serviceAdditionRepository;
        [TestInitialize]
        public void Initialize()
        {
            _serviceAdditionRepository = new ServiceAdditionRepository();
        }

        /// <summary>
        /// Case : Lấy danh sách dịch vụ
        /// </summary>
        [TestMethod]
        public void GetServiceAdditions_ShouldReturnAllServiceAdditions()
        {
            var actual = _serviceAdditionRepository.GetAll();
            int expected = 20;
            Assert.AreEqual(expected, actual.Count());
        }

        /// <summary>
        /// Case lấy danh sách dịch vụ theo món ăn: inventoryItemId = null
        /// </summary>
        [TestMethod]
        public void GetServiceAdditionsByInventoryItemId_NotExistInventoryItemId()
        {
            Guid inventoryItemId = new Guid();
            var actual = _serviceAdditionRepository.GetServiceAdditionsByInventoryItemId(inventoryItemId);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        /// <summary>
        /// Case lấy danh sách dịch vụ theo món ăn : inventoryItemId không có dữ liệu
        /// </summary> 
        [TestMethod]
        public void Test_GetServiceAdditionsByInventoryItemId_ShouldReturnEmptyData()
        {
            Guid inventoryItemId = Guid.Parse("14c0ce35-1687-459c-1acb-66948daf6128");
            var actual = _serviceAdditionRepository.GetServiceAdditionsByInventoryItemId(inventoryItemId);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        /// <summary>
        /// Case lấy danh sách dịch vụ theo món ăn : inventoryItemId có dữ liệu
        /// </summary>
        [TestMethod]
        public void Test_Test_GetServiceAdditionsByInventoryItemId_ShouldReturnDataByInventoryItemId()
        {
            Guid inventoryItemId = Guid.Parse("8f0abcb7-20db-11ec-a281-f01faf56e08c");
            var actual = _serviceAdditionRepository.GetServiceAdditionsByInventoryItemId(inventoryItemId);
            var expected = 2;
            Assert.AreEqual(expected, actual.Count());
        }
    }
}
