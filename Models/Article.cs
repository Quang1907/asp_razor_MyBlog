
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_RAZOR_5.Models
{
    //[Table("posts")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 5,  ErrorMessage = "{0} phai dai {2} den {1}")]
        [Required(ErrorMessage = "{0} phai nhap")]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Tiêu đề")]
        public string? Title { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Noi dung")]
        public string? Content { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Ngay tao")]
        public DateTime Created { get; set; }
    }
}
