using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComicShop.Models
{
    public class Comic 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

    }
}
