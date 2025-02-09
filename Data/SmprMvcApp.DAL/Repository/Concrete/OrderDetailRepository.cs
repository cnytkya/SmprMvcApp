using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private AppDbContext _appDbContext;

        public OrderDetailRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public void Update(OrderDetails entity)
        {
            _appDbContext.OrderDetails.Update(entity);
        }
    }
}