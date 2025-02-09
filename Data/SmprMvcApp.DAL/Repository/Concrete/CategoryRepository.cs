using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        //public void Save()
        //{
        //    _appDbContext.SaveChanges();
        //}

        public void Update(Category entity)
        {
            _appDbContext.Categories.Update(entity);
        }
    }
}