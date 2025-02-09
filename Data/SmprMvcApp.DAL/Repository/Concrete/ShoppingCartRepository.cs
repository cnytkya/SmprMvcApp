using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private AppDbContext _appDbContext;

        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public void Update(ShoppingCart entity)
        {
            _appDbContext.ShoppingCarts.Update(entity);
        }
    }
}