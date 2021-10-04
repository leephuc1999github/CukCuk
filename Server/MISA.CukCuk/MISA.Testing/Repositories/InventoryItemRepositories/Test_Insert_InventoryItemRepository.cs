using Microsoft.VisualStudio.TestTools.UnitTesting;
using MISA.Core.Enitites;
using MISA.Core.Interfaces.Repositories;
using MISA.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Testing.Repositories.InventoryItemRepositories
{
    [TestClass]
    public class Test_Insert_InventoryItemRepository
    {
        private InventoryItemRepository _inventoryItemRepository = new InventoryItemRepository();
        private Picture picture = new Picture();
        private InventoryItem inventoryItem = new InventoryItem();
        private List<ServiceAddition> serviceAdditions = new List<ServiceAddition>();


        [TestInitialize]
        public void Initialize()
        {
           
        }

        #region Case success
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void InsertInventoryItem_WithAllParams()
        {
            var actual = _inventoryItemRepository.InsertInventoryItem(inventoryItem, picture, serviceAdditions);
            Assert.AreEqual(0, actual);
        }
        #endregion

        #region Case fail

        #endregion
    }
}
