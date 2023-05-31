using AutoMapper;
using CustomerInfo.Helper;
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
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll(DataTableAjaxPostModel model)
        {
            int count;
           var data= customerService.GetAllCustomer(model,out count);
              return Json(new
            {
                draw = model.draw,
                recordsTotal = count,
                recordsFiltered = count,
                data = data

              });
        }

        [HttpPost]
     
        public ActionResult Create(CustomerModel model)
        {

        bool response= customerService.addCustomers(model);
            return Json(response);
        }



        [HttpPut]
        public ActionResult edit(int id)
        {

        var customerModel=  customerService.edit(id);
            return Json(customerModel);
        }

        [HttpPost]
       
        public ActionResult update(CustomerModel customerModel)
        {
           
             var response= customerService.update(customerModel);
                return Json(response);
            
           
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
          var response=  customerService.delete(id);
            return Json(response);
        }

        public ActionResult AddInvoice(InvoiceModel model)
        {
        var  response=  customerService.addInvoice(model);
            return Json(response);
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
               // worksheet.Cells[i + 1, 1].Value = data[i].;
               // worksheet.Cells[i + 1, 2].Value = data[i].;
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
