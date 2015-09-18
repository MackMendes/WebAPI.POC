using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WebAPI_POC.Controllers;

namespace WebAPI_POC.Test.Controllers
{
    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        public void Get()
        {
            ClienteController controller = new ClienteController();
            IEnumerable<Cliente> result = controller.Get().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void GetByID()
        {
            ClienteController controller = new ClienteController();

            int id = 1;

            Cliente result = controller.Get(id).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }
    }
}
