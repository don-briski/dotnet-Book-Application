using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class CategoryController : Controller
  {
   // private readonly ICategoryRepository _categoRepo;
    private readonly IUnitofWork _unitofWork;
    public CategoryController( IUnitofWork unitofWork)
    {
     // _categoRepo = db;
      _unitofWork = unitofWork;
    }

    public IActionResult Index()
    {
      List<Category> objCategoryList = _unitofWork.Category.GetAll().ToList();
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
        _unitofWork.Category.Add(obj);
        _unitofWork.Save();
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
      Category? categoryObj = _unitofWork.Category.Get(u => u.Id == id);
      // Category? categoryObj1 = _db.Categories.FirstOrDefault(u => u.Id == id);
      // Category? categoryObj2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
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
      _unitofWork.Category.Update(obj);
      _unitofWork.Save();
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

    Category? categoryobj = _unitofWork.Category.Get( U => U.Id ==  id );
    if( categoryobj == null ) 
    {
      return NotFound();
    }

    return View(categoryobj);
  }
  [HttpPost, ActionName("Delete")]
  public IActionResult DeletePost(int? id )
  {
    Category? categoryobj = _unitofWork.Category.Get( u => u.Id == id);
    if (categoryobj == null)
    {
      return NotFound();
    }
    _unitofWork.Category.Remove(categoryobj);
    _unitofWork.Save();
    TempData["success"] = "Category deleted sucessfully.";
    return RedirectToAction("Index", "Category");
  }


  }
 
}