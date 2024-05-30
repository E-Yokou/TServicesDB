using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TServicesDB.Models
{
    public class TSTicket
    {
        public int TSTicketID { get; set; }
            
        [Display(Name = "Номер Пути")]
        public string number_route { get; set; }

        [Display(Name = "Почта Клиента")]
        public string mail_client { get; set; }

        [Display(Name = "Остановка")]
        public string stopover { get; set; }

        [Display(Name = "Время Покупки Билета")]
        public DateTime date_sale { get; set; }

        [Display(Name = "Время Отправления")]
        public string date_route { get; set; }

        [Display(Name = "Цена")]
        public int price { get; set; }

        public int TSBusSecondId { get; set; }
        public TSBus? TSBus { get; set; }
    }
}
