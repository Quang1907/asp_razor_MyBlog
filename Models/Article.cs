
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_RAZOR_5.Models
{
    //[Table("posts")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string? Content { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Created { get; set; }
    }
}
