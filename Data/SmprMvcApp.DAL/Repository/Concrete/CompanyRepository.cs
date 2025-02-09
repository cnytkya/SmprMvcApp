using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private AppDbContext _appDbContext;

        public CompanyRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        //public void Save()
        //{
        //    _appDbContext.SaveChanges();
        //}

        public void Update(Company entity)
        {
            _appDbContext.Companies.Update(entity);
        }
    }
}