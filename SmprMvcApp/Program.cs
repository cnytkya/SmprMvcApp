//buras� istek hatt� (request pipeline)'n� olu�turur. Globalde genellikle bu bir boru hatt�na benzetilir.
//App' ilk istek at�ld���nda iste�e binaen a�a��daki Middleware(ara yaz�l�m) yap�lar� devreye girer.

using Microsoft.EntityFrameworkCore;
using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Concrete;
using SmprMvcApp.DAL.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmprMvcApp.Common;
using Stripe;

var builder = WebApplication.CreateBuilder(args);//Bu sat�r, WebApplication s�n�f�n�n bir �rne�ini olu�turur ve uygulamay� ba�latmak i�in yap�land�rma i�lemlerine ba�lar. CreateBuilder(args) metodu, uygulaman�n yap�land�rmas�n� ve ba��ml�l�klar�n� (dependencies) ayarlamak i�in kullan�l�r.

//NOT:Eskiden iki dosya kullan�l�rd�. Program.cs ve Startup.cs. Daha sonra ikisi Program.cs te birle�tirildi.

// Add services to the container.
builder.Services.AddControllersWithViews();//Bu sat�r, Dependency Injection (ba��ml�l�k enjeksiyonu) sistemi arac�l���yla MVC mimarisi i�in gerekli servisleri ekler. AddControllersWithViews(), uygulaman�n hem Controller s�n�flar�n� (yani i� mant���n� y�neten s�n�flar) hem de View (g�r�n�m) dosyalar�n� (UI) kullanmas�n� sa�lar.

//EF Core kullanarak veritaban�na ba�lan. Burdaki i�lemin ad� DI'dir.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));

//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = $"/Identity/Account/Login";
    opt.LogoutPath = $"/Identity/Account/Logout";
    opt.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddRazorPages();

// CORS policy ekleme
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

//Scoped ya�am s�resi, ayn� HTTP iste�i boyunca ayn� nesnenin yeniden kullan�lmas�n� sa�lar.
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
//builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
//builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

var app = builder.Build();//Buras�, uygulaman�n yap�land�rmas�n� tamamlay�p uygulamay� ba�lat�r. Build() metodu, uygulaman�n �al��maya haz�r hale gelmesini sa�lar ve app nesnesi �zerinden yap�land�rma i�lemlerine devam edilir.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())//Bu ko�ul, uygulaman�n �al��ma ortam�n� kontrol eder. E�er uygulama Development (Geli�tirme) ortam�nda de�ilse (�rne�in, Production veya Staging ortam�nda ise), hata y�netimi i�in UseExceptionHandler ve UseHsts gibi y�ntemler uygulan�r. Bu ko�ul, geli�mi� hata y�netimi ve g�venlik ayarlar�n� �retim ortam�nda etkinle�tirir.
{
    app.UseExceptionHandler("/Home/Error");//Bu sat�r, bir hata durumunda /Home/Error adresine y�nlendirme yapar. Bu sayede kullan�c�lar hata ekran�n� g�r�r ve daha g�venli bir hata y�netimi sa�lan�r.
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(//Bu k�s�m, uygulama i�in bir varsay�lan rota tan�mlar.
    name: "default",//Rota ad� olarak "default" verilmi�tir.
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");//Rota deseni olarak. Ayn� zamanda buras� bir endpoint olu�turur. controller=Home: Varsay�lan controller Home olarak ayarlanm��t�r. action=Index: Varsay�lan aksiyon Index olarak belirlenmi�tir. {id?}: id parametresi iste�e ba�l�d�r (? i�areti bu durumu belirtir) ve route i�inde bir de�ere sahip olmasa bile route �al��maya devam eder.

app.Run();//Bu sat�r, uygulaman�n �al��mas�n� ba�lat�r ve uygulama burada s�rekli olarak istekleri dinlemeye ba�lar.