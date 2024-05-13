using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LABB_4.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Customer måste ha ett namn!")]
        [DisplayName("Full Name")]
        [StringLength(maximumLength: 60, ErrorMessage = "Namn får inte överskrida 60 bokstäver!")]
        public string CustomerName { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Zip Code")]

        public string ZipCode { get; set; }

        public ICollection<BookLoan>? BookLoans { get; set; }

    }
}
