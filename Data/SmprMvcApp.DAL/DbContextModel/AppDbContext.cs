using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.DbContextModel
{
    public class AppDbContext : IdentityDbContext<IdentityUser> //AppDbContext, IdentityDbContext'in tüm özelliklerini ve davranışlarını devralır.
    {
        //connection string'i bir parametre olarak alıp içeriye aktarmamız gerekiyor. bunun için kurucu metodu oluşturmamız gerekir.
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Burada yapılandırdığımız seçenekler DbContext'in temel sınıfına aktarılacaktır.
            //Veritabanına bağlanacağımızı bizim container'a yani Program.cs'e söylememiz gerekiyor.
            //builder.Services.AddDbContext<AppDbContext>(); => Program.cs'e Dipendency Injection uygulayarak uygulamanın veritabanını ile iletişimini sağladık.
        }

        //Hazır kayıt(seed data)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//base.OnModelCreating(modelBuilder);, IdentityDbContext'te tanımlanmış olan varsayılan yapılandırmaların korunmasını sağlar. base ifadesi, IdentityDbContext'in OnModelCreating metodunu çağırır. Eğer bu satır çağrılmazsa, IdentityDbContext'in sağladığı varsayılan yapılandırmalar (kullanıcı tabloları, roller, kullanıcı rollerinin eşleştirilmesi) uygulanmaz ve kimlik doğrulama sistemi düzgün çalışmaz.

            /* modelBuilder.Entity<Category>().HasData(
                 new Category { Id = 1, Name = "Roman", DisplayOrder = 1 },
                 new Category { Id = 2, Name = "Hikaye", DisplayOrder = 2 },
                 new Category { Id = 3, Name = "Senaryo", DisplayOrder = 3 }
             );
             modelBuilder.Entity<Product>().HasData(
                 new Product
                 {
                     Id = 1,
                     Title = "HP Victus",
                     Author = "Cüneyt",
                     Description = "HP Victus 16-R1001NT Intel Core i7 14700HX 16 GB 1 TB SSD",
                     ISBN = "ASS231D65F45DF",
                     Price = 5,
                     ListPrice = 5,
                     Price50 = 5,
                     Price100 = 5,
                     CategoryId = 1,
                     ImageUrl = "",
                 },
                 new Product
                 {
                     Id = 2,
                     Title = "Lenova Thingpad",
                     Author = "Kasım",
                     Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                     ISBN = "ASS231D65F45DF",
                     Price = 5,
                     ListPrice = 5,
                     Price50 = 5,
                     Price100 = 5,
                     CategoryId = 2,
                     ImageUrl = ""
                 },
                 new Product
                 {
                     Id = 3,
                     Title = "Lenova Thingpad",
                     Author = "Kasım",
                     Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                     ISBN = "ASS231D65F45DF",
                     Price = 5,
                     ListPrice = 5,
                     Price50 = 5,
                     Price100 = 5,
                     CategoryId = 3,
                     ImageUrl = ""
                 }
             );*/
        }
    }
}