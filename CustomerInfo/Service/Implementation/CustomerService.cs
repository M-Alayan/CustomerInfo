using AutoMapper;
using CustomerInfo.Data;
using CustomerInfo.Helper;
using CustomerInfo.Models;
using CustomerInfo.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public bool addCustomers(CustomerModel customers)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customers);
                context.Customers.Add(customer);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
           
        }
        public List<CustomerModel> liCustomers()
        {
            List<Customer> liCustomers = context.Customers.ToList();
            var liCustomerModel = _mapper.Map<List<CustomerModel>>(liCustomers);
            return liCustomerModel;
        }


        public bool delete(int Id)
        {
            try
            {
                Customer customer = context.Customers.Find(Id);
                context.Customers.Remove(customer);
                context.SaveChanges();
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public CustomerModel edit(int id)
        {

            Customer customer = context.Customers.Find(id);
            var customerModel = _mapper.Map<CustomerModel>(customer);
            customerModel.Id = id;
            return customerModel;
        }
        public bool update(CustomerModel customerModel)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerModel);
                context.Customers.Attach(customer);
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool addInvoice(InvoiceModel invoiceModel)
        {
            try
            {
                invoiceModel.SubTotal=(invoiceModel.Price *invoiceModel.Quantity);
                var invoice = _mapper.Map<Invoices>(invoiceModel);
                context.Invoices.Add(invoice);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
         public List<InvoiceModel> GetInvoices(int customerId)
            {
         
            var liInvoices = context.Invoices.Where(x => x.CustomerId == customerId).ToList();
            var liInvoicesModel = _mapper.Map<List<InvoiceModel>>(liInvoices);
            return liInvoicesModel;
            }
        public  List<CustomerModel> GetAllCustomer(DataTableAjaxPostModel model, out int count)
        {
            var result = new List<CustomerModel>();
            try
            {

                
                
                    var sortBy = "Id";
                    var sortDir = false;
                    var searchKey = (model.CustomerName != null) ? model.CustomerName : null;
                    var take = model.length;
                    var skip = model.start;


                    var Data = context.Customers.AsQueryable(); 
                    if (model.order .Count>0 )
                    {
                        sortBy = model.columns[model.order[0].column].name;
                        sortDir = model.order[0].dir.ToLower() == "asc";
                    }
                    if (!string.IsNullOrWhiteSpace(searchKey))
                    {
                        Data = Data.Where(x => x.CustomerName.Contains(searchKey));
                    }

                    switch (sortBy)
                    {
                        case "CustomerName":
                            Data = sortDir ? Data.OrderBy(x => x.CustomerName)
                                                         : Data.OrderByDescending(x => x.CustomerName);

                            break;
                        case "Mobile":
                            Data = sortDir ? Data.OrderBy(x => x.Mobile)
                                                        : Data.OrderByDescending(x => x.Mobile);
                            break;
                        case "Address":
                            Data = sortDir ? Data.OrderBy(x => x.Address)
                                                  : Data.OrderByDescending(x => x.Address);
                            break;

                        default:
                            Data = sortDir ? Data.OrderBy(x => x.Id)
                                            : Data.OrderByDescending(x => x.Id);
                            break;
                    }
                    count = Data.Count();
                    Data = Data.Skip(skip).Take(take);
                    result = _mapper.Map<List<CustomerModel>>(Data.ToList());
                
                return result;
            }
            catch (Exception ex)
            {
                
                count = 0;
                return new List<CustomerModel>();
            }

        }


    }
}
