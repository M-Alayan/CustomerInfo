using CustomerInfo.Data;
using CustomerInfo.Models;

namespace CustomerInfo.Service.Interfaces
{
    public interface ICustomerService
    {
        void addCustomers(CustomerModel customers);
        List<CustomerModel> liCustomers();
        CustomerModel delete(int id);
        CustomerModel edit(int id);
        void update(CustomerModel customers);
        void addInvoice(InvoiceModel invoiceModel);
        List<InvoiceModel> GetInvoices(int customerId);

    }
}
