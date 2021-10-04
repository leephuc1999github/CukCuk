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
    public class TestUnitRepository
    {
        private IUnitRepository _unitRepository;
        [TestInitialize]
        public void Initialize()
        {
            _unitRepository = new UnitRepository();
        }

        [TestMethod]
        public void GetUnits_ShouldReturnAllUnits()
        {
            var actual = _unitRepository.GetAll();
            int expected = 15;
            Assert.AreEqual(expected, actual.Count());
        }
    }
}
