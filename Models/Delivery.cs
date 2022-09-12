using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComicShop.Models
{

    public class Delivery
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public Storage Storage { get; set; }

        [Required]
        public virtual ICollection<ComicStorage> Сomics { get; set; } = new List<ComicStorage>();
    }
}
