using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace semestralni_Navratilova.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [RegularExpression(@"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$", ErrorMessage = "ISBN má nesprávný formát. Př. 978-951-45-9695-7")]
        public string? ISBN { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Author { get; set; }

        public string? Binding { get; set; }

        public string? Genre { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        [Range(0, int.MaxValue, ErrorMessage = "Počet stran nesmí být záporné číslo.")]
        public int? Pages { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Publisher { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Language { get; set; }

        public string? Translator { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        [Range(1000, 2100, ErrorMessage = "Zadejte validní rok.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        [Range(0, int.MaxValue, ErrorMessage = "Počet ks nesmí být záporné číslo.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public int? AvailablePieces { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}