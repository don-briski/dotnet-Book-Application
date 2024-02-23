
using BulkyRazor.Models;
using BulkyWebRazorApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazorApp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _db.Categories.Find(id);
        }
        public IActionResult OnPost()
        {
            // if (Category != null)
            // {
            //     _db.Categories.Remove(Category);
            //     _db.SaveChanges();
            //     TempData["success"] = "Category deleted successfully";
            // }
            Category? obj = _db.Categories.Find(Category.Id);
            if(obj == null) 
            {
                return NotFound();
            } 
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}