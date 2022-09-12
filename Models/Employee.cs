using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicShop.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Column(TypeName = "Date")]
        DateTime DateOfBirth { get; set; }

        int WorkTime { get; set; }

        [Required]
        Position Position { get; set; }
    }
}
