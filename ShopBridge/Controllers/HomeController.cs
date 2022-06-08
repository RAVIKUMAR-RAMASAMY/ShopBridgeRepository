using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopBridge.Models;
using ShopBridge.Server;

namespace ShopBridge.Controllers
{
    public class HomeController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net  
        public ShopBridgeDBEntities dbContext ; //Declaring DBcontext
        public Product product; //Declare obj for server module
        
        public HomeController()
        {
            dbContext = new ShopBridgeDBEntities();
            product = new Product(dbContext);
        }
        public HomeController(ShopBridgeDBEntities db )
        {
            dbContext = db;
            product = new Product(dbContext);

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OurProducts()
        {
            List<Inventory> products = new List<Inventory>();
            try
            {
                ViewBag.Message = "Your Products are Here";
                products = product.GetAllProducts();
            }
            catch (Exception ex)
            {

                logger.Error(ex.ToString());
            }
                return View(products);

        }
        public ActionResult Create()
        {
            //Create new object for item
            var item = new Inventory();
            return View(item);
        }

        [HttpPost] //Create new Product item
        public async Task<ActionResult> Create(Inventory item)
        {
            try
            {
                item.Id = product.GetAllProducts().Count() + 1;
                if (!ModelState.IsValid)
                {
                    return View(item);
                }

                await product.AddItem(item);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }

            return  RedirectToAction("OurProducts");
        }

        public ActionResult Edit(int id)
        {
            
            //getting a product from db
            var item = product.SelectItem(id);

            return View(item);
        }

        [HttpPost] //update the Product item
        public async Task<ActionResult> Edit(Inventory item)
        {
            try
            {



                if (!ModelState.IsValid)
                {
                    return View(item);
                }

                await product.ModifyItem(item);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return RedirectToAction("OurProducts");
        }


        public ActionResult Delete(int id)
        {

            //getting a product from db
            var item = product.SelectItem(id);

            return View(item);
        }

        [HttpPost] //update the Product item
        public async Task<ActionResult> Delete(Inventory item)
        {
            try
            {
                await product.DeleteItem(item.Id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());

            }
            return RedirectToAction("OurProducts");
        }


     
    }
}