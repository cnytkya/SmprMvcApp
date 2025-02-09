using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.Common;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using SmprMvcApp.EntityLayer.ViewModels;
using System.Security.Claims;

namespace SmprMvcApp.Areas.Customer.Controllers
{
	[Area("Customer")] //bu attribute, controller'ın bir area'ya ait olduğunu belirti.
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		//alışveriş sepetiyle ilgili dataları View(görünüm) katmanına taşımak için bir ViewModel.
		[BindProperty]
		public ShoppingCartViewModel ShoppingCartVM { get; set; }

		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product"),
				OrderHeader = new()
			};
			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}

		private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
		{
			if (shoppingCart.Count <= 50)
			{
				return shoppingCart.Product.Price;
			}
			else
			{
				if (shoppingCart.Count <= 100)
				{
					return shoppingCart.Product.Price50;
				}
				else
				{
					return shoppingCart.Product.Price100;
				}
			}
		}

		public IActionResult Plus(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
			cartFromDb.Count += 1;
			_unitOfWork.ShoppingCart.Update(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Minus(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
			if (cartFromDb.Count <= 1)
			{
				//sepetteki cart sayısını azalttığımızda en az 1'e kadar gidecek şekilde yapmamız gerekiyor. Eğer 1'in altına düşerse otomatik olarak cartı da sepetten silsin.
				_unitOfWork.ShoppingCart.Remove(cartFromDb);
			}
			else
			{
				cartFromDb.Count -= 1;
				_unitOfWork.ShoppingCart.Update(cartFromDb);

			}
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Remove(int cartId)
		{
			var cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCart.Remove(cartFromDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Summary(int cartId)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product"),
				OrderHeader = new()
			};

			ShoppingCartVM.OrderHeader.AppUser = _unitOfWork.AppUser.Get(a => a.Id == userId);
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.AppUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.AppUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.AppUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.AppUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.AppUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.AppUser.PostalCode;

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
		}
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.AppUserId == userId, includeProperties: "Product");

			ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartVM.OrderHeader.AppUserId = userId;

			AppUser appUser = _unitOfWork.AppUser.Get(u => u.Id == userId);

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				cart.Price = GetPriceBasedOnQuantity(cart);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

			if (appUser.CompanyId.GetValueOrDefault() == 0)
			{
				ShoppingCartVM.OrderHeader.PaymentStatus = Roles.PaymentStatusPending;
				ShoppingCartVM.OrderHeader.OrderStatus = Roles.StatusApproved;
			}
			else
			{
				//company kullanıcıları için
				ShoppingCartVM.OrderHeader.PaymentStatus = Roles.PaymentStatusDelayedPayment;
				ShoppingCartVM.OrderHeader.OrderStatus = Roles.StatusApproved;
			}
			_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
			_unitOfWork.Save();

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				OrderDetails orderDetails = new()
				{
					ProductId = cart.ProductId,
					OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count
				};
				_unitOfWork.OrderDetail.Add(orderDetails);
				_unitOfWork.Save();
			}

			if (appUser.CompanyId.GetValueOrDefault() == 0)
			{
				var domain = "https://localhost:7287/";
				var options = new Stripe.Checkout.SessionCreateOptions
				{
					SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
					CancelUrl = domain + "Customer/Cart/Index",
					LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
					{
						new Stripe.Checkout.SessionLineItemOptions
					    {
					      Price = "price_1MotwRLkdIwHu7ixYcPLm5uZ",
					      Quantity = 2,
					    },
					},
					Mode = "payment",
				};
				var service = new Stripe.Checkout.SessionService();
				service.Create(options);
			}
			return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
		}

		public IActionResult OrderConfirmation(int id)//sipariş onay sayfası için bir action
		{
			return View(id);
		}
	}
}
