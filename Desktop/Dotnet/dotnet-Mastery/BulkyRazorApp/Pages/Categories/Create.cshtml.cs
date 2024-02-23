using BulkyRazor.Models;
using BulkyWebRazorApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazorApp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
private readonly  ApplicationDbContext _db; 

public Category Category {get; set;}
        public CreateModel (ApplicationDbContext db) 
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid) 
            {
                return Page();
            }

            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }

    //     public async Task<IActionResult> OnPostAsync()
    //     {

    //         if(!ModelState.IsValid) 
    //         {
    //             return Page();
    //         }

    //         await _db.Categories.AddAsync(Category);
    //         await _db.SaveChangesAsync();
    //         return RedirectToPage("Index");
    //     }
    // }
 
}


}
