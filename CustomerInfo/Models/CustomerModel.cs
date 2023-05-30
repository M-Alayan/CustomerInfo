using System.ComponentModel.DataAnnotations;

namespace CustomerInfo.Models
{
    public class CustomerModel
    {
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
    }
}
