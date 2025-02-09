using SmprMvcApp.DAL.DbContextModel;

// Veritabanı bağlamı için gerekli olan `AppDbContext` sınıfını kullanabilmek için bu namespace dahil ediliyor.

using SmprMvcApp.DAL.Repository.Interface;

// Repository interface'lerinin (örneğin `ICategoryRepository`, `IProductRepository`, `IUnitOfWork`) erişilebilir olması için bu namespace kullanılıyor.

// Temel C# kütüphanelerini ve görevlerle (Task) çalışmak için gerekli sınıfları sağlar.

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IAppUserRepository AppUser { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _appDbContext = context;

            Category = new CategoryRepository(context);

            Product = new ProductRepository(context);
            Company = new CompanyRepository(context);
            ShoppingCart = new ShoppingCartRepository(context);
            AppUser = new AppUserRepository(context);
            OrderDetail = new OrderDetailRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}