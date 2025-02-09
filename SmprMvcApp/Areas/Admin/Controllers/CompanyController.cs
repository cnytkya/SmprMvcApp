using Microsoft.AspNetCore.Mvc;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> CompanysList = _unitOfWork.Company.GetAll().ToList();
            return View(CompanysList);
        }

        #region
        /*[HttpGet]
        public IActionResult Create()
        {
            *//*IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.
            GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });*//*
            //ViewBag kullanımı
            //ViewBag.CategoryList = CategoryList;
            //ViewData kullanımı
            //ViewData["CategoryList"] = CategoryList;
            CompanyViewModel CompanyViewModel = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,//Text: Kullanıcıya gösterilecek olan metin (Name).
                    Value = c.Id.ToString()//ToString() ile bu ID, metin formatına dönüştürülür, çünkü Value özelliği bir string türündedir. <option value="3">...</option>
                }),
                Company = new Company()
            };
            return View(CompanyViewModel);
        }

        [HttpPost]
        public IActionResult Create(CompanyViewModel CompanyViewModel)
        {
            if (ModelState.IsValid)//server side validations
            {
                _unitOfWork.Company.Add(CompanyViewModel.Company);
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully"; //eğer ekleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            else
            {
                CompanyViewModel.CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(CompanyViewModel);
            }
        }*/

        /*[HttpGet]
        public IActionResult Update(int? id)//Update ederken obj'yi id'ye göre çağıracağız. Nullable olabilir.
        {
            Company? Company = _unitOfWork.Company.Get(c => c.Id == id);
            if (id == null || id == 0)//Eğer böyle bir kayıt yoksa, null döndür.
            {
                return NotFound();
            }

            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        [HttpPost]
        public IActionResult Update(Company obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully"; //eğer güncelleme başarılı ise bana success mesajı versin.
                return RedirectToAction("Index");
            }
            return View();
        }*/
        #endregion

        //create ve update metotlarını tek bir fonksiyonda birleştirdim.
        [HttpGet]
        public IActionResult Upsert(int? id) //Update + Insert: Combine ettik yani iki metodu birleştirdik.
        {
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company company = _unitOfWork.Company.Get(c => c.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)//server side validations
            {
                //create ve update için karar yapısını kuralım
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }
                _unitOfWork.Save();
                if (company.Id == 0)
                {
                    //message for create
                    TempData["success"] = "Company created successfully"; //eğer ekleme başarılı ise bana success mesajı versin.
                }
                else
                {
                    //message for update
                    TempData["success"] = "Company updated successfully"; //eğer güncelleme başarılı ise bana success mesajı versin.
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(company);
            }
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var company = _unitOfWork.Company.Get(p => p.Id == id);

            if (company == null)
            {
                TempData["error"] = "Company not found.";
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var company = _unitOfWork.Company.Get(p => p.Id == id);

            if (company == null)
            {
                TempData["error"] = "Company not found.";
                return RedirectToAction("Index");
            }

            // Ürünü veritabanından sil
            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();

            TempData["success"] = "Company deleted successfully.";
            return RedirectToAction("Index");
        }

        #region API CALLS

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    List<Company> CompanysList = _unitOfWork.Company.GetAll(includeProperties: "Category").ToList();
        //    return Json(new { data = CompanysList });
        //}

        #endregion
    }
}