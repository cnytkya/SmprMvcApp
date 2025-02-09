using SmprMvcApp.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmprMvcApp.EntityLayer.Entities
{
    public class Category
    {
        //[Key]=> eğer Id olarak tanımlaycak olursak buna gerek yok
        /*[Key]*/
        public int Id { get; set; } //Id => Primary key, otomotik artan tam sayı olarak tanımladık.

        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Enter a value between 1 and 100")] //server side validations. burda kullanıcı 1 ile 100 arasında değer girebilir diyeceğiz. Bunu aştığında ya da 1'den daha küçük bir değer girdiğinde buna izin verilmesin. Buna sunucu tarafı validasyonları denir.
        public int DisplayOrder { get; set; }
    }
}