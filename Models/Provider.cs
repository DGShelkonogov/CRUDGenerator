using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComicShop.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryProvider { get; set; }

        [Required]
        public virtual ICollection<Comic> Сomics { get; set; } = new List<Comic>();

    }
}
