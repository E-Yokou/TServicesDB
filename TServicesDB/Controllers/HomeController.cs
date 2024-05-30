using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using TServicesDB.Models;

namespace CourseWork_RSOD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TServicesDBContext _context;

        public HomeController(ILogger<HomeController> logger, TServicesDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult ExportToExcelTSStopover()
        {
            byte[] result;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new MemoryStream()))
            {
                var worksheet = package.Workbook.Worksheets.Add("TSStopover");

                // Add headers
                worksheet.Cells[1, 1].Value = "Отбытие";
                worksheet.Cells[1, 3].Value = "Прибытие";
                worksheet.Cells[1, 5].Value = "Информация о существующих маршрутах";
                
                // Add data
                var stopovers = _context.TSStopovers.ToList();
                int row = 2;
                int count = 0;
                foreach (var stopover in stopovers)
                {
                    worksheet.Cells[row, 1].Value = stopover.start_city;
                    worksheet.Cells[row, 2].Value = " - ";
                    worksheet.Cells[row, 3].Value = stopover.name_stopover;
                    count++;
                    row++;
                }

                worksheet.Cells[2, 5].Value = "Всего маршрутов: " + count;

                result = package.GetAsByteArray();
            }

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TSStopover.xlsx");
        }

        public IActionResult ExportToExcelTSTicket()
        {
            byte[] result;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new MemoryStream()))
            {
                var worksheet = package.Workbook.Worksheets.Add("TSTicket");

                // Add headers
                worksheet.Cells[1, 1].Value = "Номер Маршрута";
                worksheet.Cells[1, 2].Value = "Почта клиента";
                worksheet.Cells[1, 3].Value = "Время отбытия";
                worksheet.Cells[1, 4].Value = "Время транзакции";
                worksheet.Cells[1, 5].Value = "Цена билета";

                // Add data
                var tickets = _context.TSTickets.ToList();
                int row = 2;
                int count = 0;
                foreach (var ticket in tickets)
                {
                    worksheet.Cells[row, 1].Value = ticket.number_route;
                    worksheet.Cells[row, 2].Value = ticket.mail_client;
                    worksheet.Cells[row, 3].Value = ticket.date_route;
                    worksheet.Cells[row, 4].Value = ticket.date_sale.ToString("dd-MM-yyyy HH:mm:ss"); ;
                    worksheet.Cells[row, 5].Value = ticket.price;

                    count += ticket.price;
                    row++;
                }

                worksheet.Cells[row, 5].Value = "Итого: " + count;

                result = package.GetAsByteArray();
            }

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TSTicket.xlsx");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}