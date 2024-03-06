using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Bulky.Repository.IRepository;

namespace Bulky.Area.Customer.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitofWork _unitofWork;

    public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
    {
        _logger = logger;
        _unitofWork = unitofWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitofWork.Product.GetAll(includeProperties: "Category");
        return View(productList);
    }

       public IActionResult Details (int productId)
    {
        Product product = _unitofWork.Product.Get( u => u.Id == productId, includeProperties: "Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
