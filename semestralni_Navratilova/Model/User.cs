using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace semestralni_Navratilova.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Birthday { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? Adresse { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Toto pole je povinné!")]
        public string? DateOfRegistration { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}