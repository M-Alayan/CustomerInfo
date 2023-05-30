using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerInfo.Data
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public int Quantity { get; set; }
        public int Description { get; set; }
        public int CustomerId { get; set; }
        public float? Discount { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer customer { get; set; }
    }
}
