using DemoApp.Controllers;
using DemoApp.Interfaces.Managers;
using DemoApp.Managers;
using DemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoAppTest
{
    [TestClass]
    public class BinlistControllerTest
    {
        public BinlistControllerTest()
        {
                
        }

        [TestMethod]
        public async Task BinLookupTest()
        {
            var id = "1234";
            var binDetailsResult = new Bin() { id = id };
            var mockBinlistManager = new Mock<IBinlistManager>();

            mockBinlistManager.Setup(a => a.GetBinDetails(It.IsAny<string>())).ReturnsAsync(binDetailsResult);
            var binlistController = new BinlistController(mockBinlistManager.Object);

            var result = await binlistController.Lookup(id) as OkObjectResult;

            Assert.IsNotNull(result?.Value);
            Assert.IsInstanceOfType(result.Value, typeof(Bin));
            Assert.AreEqual(200, result?.StatusCode);


        }

        [TestMethod]
        public async Task HitCountTest()
        {
            var id = "1234";
            var binCountResult = new BinCountModel() { success = true, size = 5 };
            var mockBinlistManager = new Mock<IBinlistManager>();

            mockBinlistManager.Setup(a => a.GetCount()).ReturnsAsync(binCountResult);
            var binlistController = new BinlistController(mockBinlistManager.Object);

            var result = await binlistController.HitCount(id) as OkObjectResult;

            Assert.IsNotNull(result?.Value);
            Assert.IsInstanceOfType(result.Value, typeof(BinCountModel));
            Assert.AreEqual(200, result?.StatusCode);


        }
    }
}
