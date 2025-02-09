using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.Common;
using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        /*private readonly AppDbContext _appDbContext;*/ //private readonly AppDbContext _appDbContext; ifadesi, bir sınıf içinde AppDbContext türünde yalnızca okunabilir (readonly) bir alan tanımlar. Bu tür bir alan genellikle bağımlılık enjeksiyonu (Dependency Injection) ile veri tabanı işlemlerini yapmak için kullanılır.

        //public CategoryController(AppDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}

        //Biz artık bir arayüz kullanarak veritabanına bağlanacağız. yani doğrudan veritabanını yukarıdaki gibi kullanmak esneklik açısından doğru değildir. Bir değişiklik yaparken arayüzden yapmak daha mantıklı olur ve kod yapısını bozmamış oluruz. aşağıdaki gibi kullandığımızda daha esnek bir yapı kurmuş oluruz.
        //private readonly ICategoryRepository _categoryRepository;

        //public CategoryController(ICategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // Veritabanından kategorileri listeye çekiyoruz
            //List<Category> categoriesList = _appDbContext.Categories.ToList();
            //List<Category> categoriesList = _categoryRepository.GetAll().ToList();
            List<Category> categoriesList = _unitOfWork.Category.GetAll().ToList();

            // View'a kategoriler listesini model olarak geçiriyoruz
            return View(categoriesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder cannot have the same value");
            }
            if (ModelState.IsValid)//server side validations
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully"; //eğer ekleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)//Update ederken obj'yi id'ye göre çağıracağız. Nullable olabilir.
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            //Category? category2 = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);//LINQ sorgusu. Categories koleksiyonunu sorgula. Id sütunu verilen id değerine eşit olan ilk kaydı getir.
            //Category? category3 = _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (id == null || id == 0)//Eğer böyle bir kayıt yoksa, null döndür.
            {
                return NotFound();
            }

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category obj)
        {
            //Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder cannot have the same value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully"; //eğer güncelleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)//Update ederken obj'yi id'ye göre çağıracağız. boş değer alabilir
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            //Category? category2 = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            //Category? category3 = _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)//bir nesneyi silmek için önce o nesneyi veritabanında bulmamız gerekir. nesnenin id'sini bulup silecektir.
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully"; //eğer silme başarılı ise bana success mesajı versin.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoriesList = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = categoriesList });
        }
    }
}