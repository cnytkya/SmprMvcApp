using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.EntityLayer.ViewModels
{
    public class ProductViewModel
    {
        //Buranın amacı:Hem Product hem de CategoryList gibi farklı türden bilgileri tek bir yapı içinde birleştirerek, bir view'e (görünüme) veri taşır.
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}