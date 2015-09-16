using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI_POC.Controllers;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

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

            Assert.IsTrue(result.Count() > 0);
        }
    }
}
