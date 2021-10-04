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
    public class TestInventoryItemGroupRepository
    {
        private IInventoryItemGroupRepository _inventoryItemGroupRepository;
        [TestInitialize]
        public void Initialize()
        {
            _inventoryItemGroupRepository = new InventoryItemGroupRepository();
        }

        /// <summary>
        /// Case lấy ds nhóm thực đơn
        /// </summary>
        [TestMethod]
        public void GetInventoryItemGroups_ShouldReturnAllInventoryItemGroup()
        {
            var actual = _inventoryItemGroupRepository.GetAll();
            int expected = 9;
            Assert.AreEqual(expected, actual.Count());
        }
    }
}
