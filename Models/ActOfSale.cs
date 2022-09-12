using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicShop.Models
{
    public class ActOfSale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Required]
        public int Sum { get; set; }

        [Required]
        public virtual ICollection<ComicStorage> Comics { get; set; } = new List<ComicStorage>();
    }
}
