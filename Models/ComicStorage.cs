using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComicShop.Models
{
    public class ComicStorage 
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public Comic Comic { get; set; }
        
        [Required]
        public int Amount { get; set; }

        public ComicStorage()
        {

        }

        public ComicStorage(ComicStorage comicStorage)
        {
            Id = comicStorage.Id;
            Comic = comicStorage.Comic;
            Amount = comicStorage.Amount;
        }
    }
}
