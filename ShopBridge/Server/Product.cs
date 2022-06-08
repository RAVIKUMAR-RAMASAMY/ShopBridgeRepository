using System.Collections.Generic;
using ShopBridge.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ShopBridge.Server
{
    public class Product 
    {
        //Create obj for Dbcontext
        public ShopBridgeDBEntities db;

        public Product(ShopBridgeDBEntities dbContext)
        {
            db = dbContext;
        }


        //List all Products
        public List<Inventory> GetAllProducts()
        {
               var products = db.Inventories.ToList();

                return products;
          
        }

        //Add a Product to Inventory
        public async Task<int> AddItem( Inventory product)
        {
            db.Inventories.Add(product);
            return await db.SaveChangesAsync();

        }

        //Modify Existing Product
        public async Task<int> ModifyItem(Inventory product)
        {
            db.Entry(product).State = EntityState.Modified;
            return await db.SaveChangesAsync();

        }

        //Delete Item by id
        public async Task<int> DeleteItem(int id)
        {
            Inventory product = db.Inventories.Find(id);
            db.Inventories.Remove(product);
            return await db.SaveChangesAsync();
        }  
        
        //Select Item by id
        public Inventory SelectItem(int id)
        {
            Inventory product = db.Inventories.Find(id);
            return product;
        }
    }
}
