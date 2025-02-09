using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SmprMvcApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }

        public IActionResult ProductDetails(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost, Authorize]
        public IActionResult ProductDetails(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.AppUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.AppUserId == userId && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                //zaten daha önce ayný ürün eklenmiþse üstüne yeni count ekleyecek
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                //ilk defa ekleniyorsa yeni bir kayýt oluþturacak.
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            TempData["success"] = "Card updated successfully";
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
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
}