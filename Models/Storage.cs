using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComicShop.Models
{
    public class Storage
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public Provider Provider { get; set; }

        [Required]
        public virtual ICollection<ComicStorage> Сomics { get; set; } = new List<ComicStorage>();
    }
}
