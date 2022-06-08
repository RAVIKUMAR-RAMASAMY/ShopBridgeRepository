using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShopBridge.Models;
using ShopBridge.Controllers;
using Moq;
using System.Data.Entity;


namespace ShopBridge.Tests.Controllers
{

    [TestClass]
    public class HomeControllerTest
    {
        public Mock<ShopBridgeDBEntities> testdb;
        public Mock< DbSet<Inventory>> mockdbset;
        public Inventory addItem = new Inventory();
        public Inventory SelectItem = new Inventory();
        public List<Inventory> inventories;
        public HomeControllerTest()
        {
            Setup();
        }


       
        public void Setup()
        {
            inventories = new List<Inventory>();
            mockdbset = new Mock<DbSet<Inventory>>();
            
            Inventory testItem = new Inventory();
            for (int i = 1; i <= 10; i++)
            {
                testItem.Id = i;
                testItem.price = i * 100;
                testItem.stockCount = i * 10;
                testItem.ProdName = "Product - " + i.ToString();
                testItem.Description = "Description - " + i.ToString();
                
                inventories.Add(testItem);
               
            }
            var queryable = inventories.AsQueryable();

            addItem = testItem;
            SelectItem = testItem;
            testdb = new Mock<ShopBridgeDBEntities>();
            testdb.Setup(m => m.Inventories).Returns(mockdbset.Object);
           
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
          
            HomeController controller = new HomeController(testdb.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OurProducts()
        {
            // Arrange
            HomeController controller = new HomeController(testdb.Object);

            // Act
            ViewResult result = controller.OurProducts() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            HomeController controller = new HomeController(testdb.Object);
            int id = 10;
            // Act
            ViewResult result = controller.Edit(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        } 
        [TestMethod]
        public void Create()
        {
            // Arrange
            HomeController controller = new HomeController(testdb.Object);
            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        } 
        
        [TestMethod]
        public void Delete()
        {
            // Arrange
            HomeController controller = new HomeController(testdb.Object);
            int id = 10;
            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        } 
        
    }
}
