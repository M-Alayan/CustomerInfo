using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerInfo.Data
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Mobile { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Fax { get; set; }

        [Required]
        public string Category { get; set; }

        public virtual IList<Invoices> Invoices { get; set; }
    }
}
