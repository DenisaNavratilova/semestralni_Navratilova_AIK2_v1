using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace semestralni_Navratilova.Model
{
    public class Borrowing
    {
        [Key]
        public int BorrowingId { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        [Required]
        public DateTime DateOfBorrow { get; set; }

        public DateTime? DateOfReturn { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
