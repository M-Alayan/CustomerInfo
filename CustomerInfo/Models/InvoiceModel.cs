namespace CustomerInfo.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public int Quantity { get; set; }
        public int Description { get; set; }
        public int CustomerId { get; set; }
        public float? Discount { get; set; }
    }
}
