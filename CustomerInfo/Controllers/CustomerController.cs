using AutoMapper;
using CustomerInfo.Models;
using CustomerInfo.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Net.Mime;

namespace CustomerInfo.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerService customerService;
       
        public CustomerController(ICustomerService _customerService)
        {
            customerService= _customerService;
          
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult GetAll()
        {
           var listOfCustomer= customerService.liCustomers();
            return View();
        }

        // GET: CustomerController/Create
        [HttpPost]
     
        public ActionResult Create(CustomerModel model)
        {

            customerService.addCustomers(model);
            return View();
        }

     

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {

        var customerModel=  customerService.edit(id);
            return Json(customerModel);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel customerModel)
        {
            try
            {
                customerService.update(customerModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

     
        public ActionResult Delete(int id)
        {
            customerService.delete(id);
            return View("Index");
        }

        public ActionResult AddInvoice(InvoiceModel model)
        {
            customerService.addInvoice(model);
            return View("Index");
        }
        public ActionResult GetAllInvoices(int customerId)
        {
          var invoices=  customerService.GetInvoices(customerId);
            return Json(invoices);
        }
        public IActionResult ExportToExcel()
        {
            // Generate your data and create an Excel package using EPPlus
            List<CustomerModel> data = customerService.liCustomers();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            // Add your data to the worksheet
            for (int i = 0; i < data.Count; i++)
            {
                // Assuming YourDataModel has properties you want to export
               // worksheet.Cells[i + 1, 1].Value = data[i].Property1;
               // worksheet.Cells[i + 1, 2].Value = data[i].Property2;
                // Add more columns as needed
            }

            // Convert the Excel package to a byte array
            byte[] excelBytes = excelPackage.GetAsByteArray();

            // Return the Excel file as a download response
            string fileName = "export.xlsx";
            string contentType = MediaTypeNames.Application.Octet;
            return File(excelBytes, contentType, fileName);
        }

    }
}
