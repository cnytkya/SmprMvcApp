using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        //public void Save()
        //{
        //    _appDbContext.SaveChanges();
        //}

        public void Update(Product entity)
        {
            _appDbContext.Products.Update(entity);
        }
    }
}