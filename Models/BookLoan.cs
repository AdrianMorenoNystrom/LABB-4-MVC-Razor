using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABB_4.Models
{
    public class BookLoan
    {
        [Key]
        public int BookLoanId { get; set; }

        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Customer")]
        public int FkCustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Book")]
        public int FkBookId { get; set; }
        public Book? Book { get; set; }
    }
}
