using Microsoft.VisualStudio.TestTools.UnitTesting;
using MISA.Core.Interfaces.Repositories;
using MISA.Infrastructure.Repositories;

namespace MISA.Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void TestDemo()
        {
            var actual = "demo";
            var expected = "demo";
            Assert.AreEqual(expected, actual);
        }
    }
}
