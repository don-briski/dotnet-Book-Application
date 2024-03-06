using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Repository;
using Bulky.Repository.IRepository;

namespace Bulky;

public class ProductRepository : Repository<Product>, IProductRepository
{

private  ApplicationDbContext _db;
public ProductRepository(ApplicationDbContext db) : base(db)
{
    _db = db;
}


    // public void Save()
    // {
    //    _db.SaveChanges();
    // }

    public void Update(Product obj)
    {
     //  _db.Products.Update(obj);
     var objFromDb = _db.Products.FirstOrDefault(s => s.Id == obj.Id);
     if ( objFromDb != null) {
         objFromDb.Title = obj.Title;
         objFromDb.Description = obj.Description;
         objFromDb.CategoryId = obj.CategoryId;
         objFromDb.Author = obj.Author;
         objFromDb.ISBN = obj.ISBN;
         objFromDb.ListPrice = obj.ListPrice;
         objFromDb.Price = obj.Price;
         objFromDb.Price50 = obj.Price50;
         objFromDb.Price100 = obj.Price100;
         if (obj.ImageUrl != null) {
             objFromDb.ImageUrl = obj.ImageUrl;
         } 
        
     }
    }
}
