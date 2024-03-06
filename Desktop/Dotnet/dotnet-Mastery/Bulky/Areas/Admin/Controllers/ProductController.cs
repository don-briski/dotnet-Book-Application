using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ProductController : Controller
  {
   // private readonly IProductRepository _categoRepo;
    private readonly IUnitofWork _unitofWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController( IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
    {
     // _categoRepo = db;
      _unitofWork = unitofWork;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      List<Product> objProductList = _unitofWork.Product.GetAll(includeProperties: "Category").ToList();
     
      return View(objProductList);
    }


    public IActionResult Upsert(int? id)
    {
        // retrieve the list of categories from the database uisng IEnumerables & projections and pass it to the view using the ViewBag
        // we are using ViewModel to pass data into the view (numerous data )
      ProductVM productVM = new() {

        Product = new Product(),
        CategoryList =  _unitofWork.Category.GetAll().Select( u => new SelectListItem 
      {
        Text = u.Name,
        Value = u.Id.ToString()
      })

      };

      if (id == null || id == 0) {
        //create 
        return View(productVM);
      } else {
        // update
        productVM.Product = _unitofWork.Product.Get( u => u.Id == id);
        return View(productVM);
      }

        //ViewBag.CategoryList = CategoryList;
        // asp-items="ViewBag.CategoryList"  )"

       // ViewData["CategoryList"] = CategoryList;  // another way to pass data to the view
       // asp-items= "@(ViewData["CategoryList"] as IEnumerable<SelectListItem>
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
      if (ModelState.IsValid)
      {
        string webRootPath = _webHostEnvironment.WebRootPath;
       if ( file != null ) {
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string productPath = Path.Combine( webRootPath, @"images/product");

        if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
        {
          //delete the old image
          var oldImagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
          if ( System.IO.File.Exists(oldImagePath)) {
            System.IO.File.Delete(oldImagePath);
          }
        }

        using ( var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        {
          file.CopyTo(fileStream);
        }
        productVM.Product.ImageUrl = @"images/product/" + fileName;
       }
        // if (file != null ) {
        //   string upload = webRootPath + WC.ImagePath;
        //   string fileName = Guid.NewGuid().ToString();
        //   string extension = Path.GetExtension(file.FileName);
        //   using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
        //   {
        //     file.CopyTo(fileStream);
        //   }
        //   productVM.Product.ImageUrl = fileName + extension;
        // }
        // else
        // {
        //   if (productVM.Product.Id != 0)
        //   {
        //     Product objFromDb = _unitofWork.Product.Get(u => u.Id == productVM.Product.Id);
        //     productVM.Product.ImageUrl = objFromDb.ImageUrl;
        //   }
        // }

        if( productVM.Product.Id == 0)
        {
          _unitofWork.Product.Add(productVM.Product);
        }
        else
        {
          _unitofWork.Product.Update(productVM.Product);
        }

        _unitofWork.Product.Add(productVM.Product);
        _unitofWork.Save();
        TempData["success"] = "Product created sucessfully.";
        return RedirectToAction("Index", "Product");
      }
       else
      {
        productVM.CategoryList =  _unitofWork.Category.GetAll().Select( u => new SelectListItem 
      {
        Text = u.Name,
        Value = u.Id.ToString()
      });
       return View(productVM);
      }
     
    }

// no longer needed after using the Upsert method

  //   public IActionResult Edit(int? id) {
  //     if (id == null || id == 0)
  //     {
  //       return NotFound();
  //     }
  //     Product? ProductObj = _unitofWork.Product.Get(u => u.Id == id);
  //     // Product? ProductObj1 = _db.Categories.FirstOrDefault(u => u.Id == id);
  //     // Product? ProductObj2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
  //     if (ProductObj == null)
  //     {
  //       return NotFound();
  //     }
  //     return View(ProductObj);
  //   }
  //    [HttpPost]
  // public IActionResult Edit(Product obj)
  // {
  //   // if ( obj.Name == obj.DisplayOrder.ToString() )
  //   // {
  //   //   ModelState.AddModelError("Name", "The Name and Display Order cannot be the same." );
  //   // }

  //   if (ModelState.IsValid)
  //   {
  //     _unitofWork.Product.Update(obj);
  //     _unitofWork.Save();
  //     TempData["success"] = "Product updated sucessfully.";
  //     return RedirectToAction("Index", "Product");
  //   } 
  //   return View(obj);
  // }

  // public IActionResult Delete(int? id) 
  // {
  //   if(id == null || id == 0)
  //   {
  //     return NotFound();
  //   }

  //   Product? Productobj = _unitofWork.Product.Get( U => U.Id ==  id );
  //   if( Productobj == null ) 
  //   {
  //     return NotFound();
  //   }

  //   return View(Productobj);
  // }
  // [HttpPost, ActionName("Delete")]
  // public IActionResult DeletePost(int? id )
  // {
  //   Product? Productobj = _unitofWork.Product.Get( u => u.Id == id);
  //   if (Productobj == null)
  //   {
  //     return NotFound();
  //   }
  //   _unitofWork.Product.Remove(Productobj);
  //   _unitofWork.Save();
  //   TempData["success"] = "Product deleted sucessfully.";
  //   return RedirectToAction("Index", "Product");
  // }

  #region API CALLS
  [HttpGet]
     public IActionResult GetAll()
    {
      List<Product> objProductList = _unitofWork.Product.GetAll(includeProperties: "Category").ToList();
     
      return Json( new { data = objProductList});
    }

   [HttpDelete]
    public IActionResult Delete(int? id)
    {
      var productToBeDelete = _unitofWork.Product.Get(u => u.Id == id);
      if (productToBeDelete == null)
      {
        return Json(new { success = false, message = "Error while deleting."});
      }

          var oldImagePath = Path.Combine( _webHostEnvironment.WebRootPath,
           productToBeDelete.ImageUrl.TrimStart('\\'));

          if ( System.IO.File.Exists(oldImagePath)) {
            System.IO.File.Delete(oldImagePath);
          }

      _unitofWork.Product.Remove(productToBeDelete);
      _unitofWork.Save();
      return Json(new { success = true, message = "Delete successful."});
    }


  #endregion


  }
 
}