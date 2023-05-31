using CustomerInfo.Data;
using CustomerInfo.Helper;
using CustomerInfo.Models;

namespace CustomerInfo.Service.Interfaces
{
    public interface ICustomerService
    {
        bool addCustomers(CustomerModel customers);
        List<CustomerModel> liCustomers();
        bool delete(int Id);
        CustomerModel edit(int id);
        bool update(CustomerModel customers);
        bool addInvoice(InvoiceModel invoiceModel);
        List<InvoiceModel> GetInvoices(int customerId);
        List<CustomerModel> GetAllCustomer(DataTableAjaxPostModel model, out int count);

    }
}
