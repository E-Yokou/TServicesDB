//using OfficeOpenXml;
//using System.Net.Mail;
//using System.Net;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting.Internal;
//using OfficeOpenXml;
//using System;
//using TServicesDB.Models;

//namespace TServicesDB
//{
//    public class ReportSender
//    {
//        string file_path_template;
//        string file_path_report;
//        private readonly TServicesDBContext _context;
//        private readonly IWebHostEnvironment _appEnvironment;
//        public ReportSender(TServicesDBContext context, IWebHostEnvironment appEnvironment)
//        {
//            _context = context;
//            _appEnvironment = appEnvironment;
//        }
//        public void PrepareReport()
//        {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//            //открываем файл с шаблоном
//            using (ExcelPackage excelPackage = new ExcelPackage(file_path_template))
//            {
//                //устанавливаем поля документа
//                excelPackage.Workbook.Properties.Author = "Кокурин Я. Д.";
//                excelPackage.Workbook.Properties.Title = "Список оказанных услуг";
//                excelPackage.Workbook.Properties.Subject = "Оказанные услуги";
//                excelPackage.Workbook.Properties.Created = DateTime.Now;
//                //плучаем лист по имени.
//                ExcelWorksheet worksheet =
//               excelPackage.Workbook.Worksheets["providedServices"];
//                //получаем списко пользователей и в цикле заполняем лист данными
//                int startLine = 3;
//                List<TSDriver> tSDrivers = _context.TSDrivers.ToList();
//                foreach (TSDriver tSDriver in tSDrivers)
//                {
//                    TSDriver tSDriver1 = _context.TSDrivers.Find(tSDriver.TSDriverID);
//                    worksheet.Cells[startLine, 1].Value = startLine - 2;
//                    worksheet.Cells[startLine, 2].Value = tSDriver1.name + ' ' + tSDriver1.middleName + ' ' + tSDriver1.lastName;
//                    worksheet.Cells[startLine, 3].Value = tSDriver1.TSBus.goverment_number;
//                    startLine++;
//                }
//                //созраняем в новое место
//                excelPackage.SaveAs(file_path_report);
//            }
//        }
//    }
//}
