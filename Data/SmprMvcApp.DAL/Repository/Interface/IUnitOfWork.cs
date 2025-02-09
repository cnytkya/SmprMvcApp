using Microsoft.EntityFrameworkCore;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IAppUserRepository AppUser { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        void Save();
    }
}