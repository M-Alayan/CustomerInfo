using AutoMapper;
using CustomerInfo.Data;

namespace CustomerInfo.Models
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer,CustomerModel>();
            CreateMap<List<Customer>,List<CustomerModel>>();
            CreateMap<InvoiceModel, Invoices>();
            CreateMap<List<Invoices>, List<InvoiceModel>>();
        }
    }
}
