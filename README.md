# SmprMvcApp
SmartPro bünyesinde, eğitmenliğini yaptığım MVC projesi.
1-)İlk aşamadaki konular;
-MVC Tasarım deseni: Model,View,Controller çalışma presibi anlatıldı. Gerçek hayat örneğiyle mantığı kavrandı.
-Connection String ile veritabanına bağlanmak: "ConnectionStrings": {
  "DefaultConnection": "server=ServerName;Database=DatabaseName;Trusted_Connection=True;TrustServerCertificate=True"
},
-Views yapılanması ve Shared: ASP.NET MVC'de Views/Shared klasörü, tüm controller ve action'lar tarafından ortaklaşa kullanılabilen görünümleri (örneğin, _Layout.cshtml veya _Error.cshtml) saklamak için kullanılır.
-Model katmanı: Create Category class.
-Create AppDbContext : EF Core ile çalışma mantığı.
-Added SeedData
-Add-Migration
-Update-Database
-Controllers katmanı:Create CategoryController class and view actions.
-Burada code first yaklaşımı uygulandı.
-Category CRUD işlemleri ve View tasarımları.
-TempData ile çalışma.
-Validasyon işlemleri.

2-)İkinci aşamadaki konular;
-Generic Repository => IRepository
![image](https://github.com/user-attachments/assets/a95b4dc4-16cc-4a88-be1e-06280011d120)

-Generic Repository'i uygulama: Önce Repository de CRUD metotlarını yazıyoruz aşağıdaki örnekte olduğu gibi:
![image](https://github.com/user-attachments/assets/30c95d46-77e7-45e1-83d9-6ec1a026462b)

Bütün Modellerde kullanabileceğimiz bir generic yapı kurduk: Aşağıda ICategoryRepository'de olduğu gibi; artık bu metotları tekrar tekrar yazmama gerek kalmadı. Sadece Implement etmemiz yeter. 
![image](https://github.com/user-attachments/assets/bce353a8-d77f-4f24-a59b-9916921ecfc1)

-Veritabanı işlemleir için artık her şey hazır ve bu metotları ihtiyacımız olduğu yerde kullanabileceğiz. Son olarak; her bir Interface'i gerektiği gibi ilgili concrete class'ta uygulamamız gerekir aşağıda olduğu gibi. Ama burda bir fark var. O da generic repository'de olmayan, özel olarak tanımladığımız iki metot var. Aşağıdaki görselde Save ve Update metotlarını sadece Category'ye ait olmasını istedim. Fakat bunu daha sonra generic repository class'a dahil edebiliriz. Özetle; eğer özel bir metot kullanacak olursak aşağıdaki görselde olduğu yazabiliriz. Diğer metotları yine ICategoryRepo'dan almış olduk.
![image](https://github.com/user-attachments/assets/94658c36-7dfe-444b-acc3-38f6f8c923d4)



