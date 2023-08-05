using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PienCeramic.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(100,ErrorMessage = "Kategori ismi en fazla 100 harften oluşabilir.")]
        public string Name { get; set; }
        [Range(0, 100,ErrorMessage ="Lütfen 1 ile 100 arasında bir sayı giriniz.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
