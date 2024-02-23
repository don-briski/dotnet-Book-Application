using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
  public class CategoryController : Controller
  {
    private readonly ApplicationDbContext _db;
    public CategoryController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      List<Category> objCategoryList = _db.Categories.ToList();
      return View(objCategoryList);
    }


    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
      if ( obj.Name == obj.DisplayOrder.ToString() )
      {
        ModelState.AddModelError("Name", "The Name and Display Order cannot be the same." );
      }

      if (ModelState.IsValid)
      {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        TempData["success"] = "Category created sucessfully.";
        return RedirectToAction("Index", "Category");
      } 
      return View(obj);
    }

    public IActionResult Edit(int? id) {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      Category? categoryObj = _db.Categories.Find(id);
      Category? categoryObj1 = _db.Categories.FirstOrDefault(u => u.Id == id);
      Category? categoryObj2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
      if (categoryObj == null)
      {
        return NotFound();
      }
      return View(categoryObj);
    }
     [HttpPost]
  public IActionResult Edit(Category obj)
  {
    // if ( obj.Name == obj.DisplayOrder.ToString() )
    // {
    //   ModelState.AddModelError("Name", "The Name and Display Order cannot be the same." );
    // }

    if (ModelState.IsValid)
    {
      _db.Categories.Update(obj);
      _db.SaveChanges();
      TempData["success"] = "Category updated sucessfully.";
      return RedirectToAction("Index", "Category");
    } 
    return View(obj);
  }

  public IActionResult Delete(int? id) 
  {
    if(id == null || id == 0)
    {
      return NotFound();
    }

    Category? categoryobj = _db.Categories.Find(id);
    if( categoryobj == null ) 
    {
      return NotFound();
    }

    return View(categoryobj);
  }
  [HttpPost, ActionName("Delete")]
  public IActionResult DeletePost(int? id )
  {
    Category? categoryobj = _db.Categories.Find(id);
    if (categoryobj == null)
    {
      return NotFound();
    }
    _db.Categories.Remove(categoryobj);
    _db.SaveChanges();
    TempData["success"] = "Category deleted sucessfully.";
    return RedirectToAction("Index", "Category");
  }


  }
 
}