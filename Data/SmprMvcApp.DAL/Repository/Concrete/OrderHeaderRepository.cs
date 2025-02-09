using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private AppDbContext _appDbContext;

        public OrderHeaderRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public void Update(OrderHeader entity)
        {
            _appDbContext.OrderHeaders.Update(entity);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var orderFromDb = _appDbContext.OrderHeaders.FirstOrDefault(o=>o.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if(!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _appDbContext.OrderHeaders.FirstOrDefault(o=>o.Id == id);
            //ödeme kimliği
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
		}
	}
}