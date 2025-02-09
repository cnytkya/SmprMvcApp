using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader entity);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
    }
}