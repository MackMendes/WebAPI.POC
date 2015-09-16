using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI_POC.Controllers;
using System.Linq;

namespace WebAPI_POC.Test.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            ValuesController controller = new ValuesController();

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
