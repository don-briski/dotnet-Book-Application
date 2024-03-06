using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Repository.IRepository;

namespace Bulky.Repository
{
    public class UnitofWork : IUnitofWork
    {
       // public ICategoryRepository CategoryRepository { get; private set;}

        public ICategoryRepository Category{ get; private set;}

        public IProductRepository Product { get; private set;}

        private readonly ApplicationDbContext _db;

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
           _db.SaveChanges();
        }
    }


}

