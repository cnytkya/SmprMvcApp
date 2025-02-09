using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IOrderDetailRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails entity);

    }
}