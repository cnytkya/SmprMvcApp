using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private AppDbContext _appDbContext;

        public AppUserRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
    }
}