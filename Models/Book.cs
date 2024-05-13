using System.ComponentModel.DataAnnotations;

namespace LABB_4.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public bool IsReturned { get; set; }
        public ICollection<BookLoan>? BookLoans { get; set; }
    }
}
