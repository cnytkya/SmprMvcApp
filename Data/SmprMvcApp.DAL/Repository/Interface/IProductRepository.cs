using SmprMvcApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);

        //void Save();

        //IRepository içinde tanımlanan tüm metotlar otomatik olarak ICategoryRepository'ye dahil olur. Eğer generic bir yapı kurmasaydık her bir model için aşağıdaki gibi metotlar oluşturmak gerekird. DRY(Dont Repeat Yourself) prensibine aykırı bir durum olurdu.

        //1. DRY Prensibi Nedir?: DRY(Don't Repeat Yourself) yazılım geliştirmede bir prensiptir ve aynı kod veya mantığın birden fazla yerde tekrar edilmesinden kaçınmayı hedefler.
        //aşağıdaki metotları generic bir yapıya çevirip bütün modeller için kullanabileceğiz. Bunu genellikle IRepository adında bir arayüzde yazarız ve istediğimiz yerde bunu imlement(uygulamak) edebiliriz.
        /*

                  IEnumerable<Category> GetAll();

                  Category Get(Expression<Func<Category, bool>> filter);

                  void Add(Category entity);

                  void Update(Category entity);

                  void Save(Category entity);

                  void Remove(Category entity);

                  void RemoveRange(IEnumerable<Category> entity);

         */
    }
}