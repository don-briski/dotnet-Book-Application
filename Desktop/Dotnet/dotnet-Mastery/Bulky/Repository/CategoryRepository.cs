using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Repository;
using Bulky.Repository.IRepository;

namespace Bulky;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{

private  ApplicationDbContext _db;
public CategoryRepository(ApplicationDbContext db) : base(db)
{
    _db = db;
}


    // public void Save()
    // {
    //    _db.SaveChanges();
    // }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
      // _db.Update(obj);
    }
}
