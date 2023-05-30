using AutoMapper;
using CustomerInfo.Data;
using CustomerInfo.Models;
using CustomerInfo.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CustomerInfo.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        AppDbContext context;
        private readonly IMapper _mapper;
        public CustomerService(AppDbContext _context, IMapper mapper)
        {
            context = _context;
            _mapper = mapper;
        }
        public void addCustomers(CustomerModel customers)
        {
            var customer = _mapper.Map<Customer>(customers);
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        public List<CustomerModel> liCustomers()
        {
            List<Customer> liCustomers = context.Customers.ToList();
            var liCustomerModel = _mapper.Map<List<CustomerModel>>(liCustomers);
            return liCustomerModel;
        }

        public CustomerModel delete(int Id)
        {
            Customer customer = context.Customers.Find(Id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            var customerModel = _mapper.Map<CustomerModel>(customer);
            return customerModel;
        }
        public CustomerModel edit(int id)
        {

            Customer customer = context.Customers.Find(id);
            var customerModel = _mapper.Map<CustomerModel>(customer);
            return customerModel;
        }
        public void update(CustomerModel customerModel)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerModel);
                context.Customers.Attach(customer);
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
        public void addInvoice(InvoiceModel invoiceModel)
        {
            try
            {
                invoiceModel.SubTotal=(invoiceModel.Price *invoiceModel.Quantity);
                var invoice = _mapper.Map<Invoices>(invoiceModel);
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
           public List<InvoiceModel> GetInvoices(int customerId)
            {
         
            var liInvoices = context.Invoices.Where(x => x.CustomerId == customerId).ToList();
            var liInvoicesModel = _mapper.Map<List<InvoiceModel>>(liInvoices);
            return liInvoicesModel;
            }


        
    }
}
