using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TServicesDB.Models
{
    public class TSStopover
    {
        public int TSStopoverID { get; set; }

        [Display(Name = "Номер маршрута")]
        public int? TSRouteId { get; set; }

        [Display(Name = "Отбытие (город)")]
        public string start_city { get; set; }

        [Display(Name = "Прибытие (город)")]
        public string end_city { get; set;}

        [Display(Name = "Имя остановки")]
        public string name_stopover { get; set; }

        [Display(Name = "Цена")]
        public int price { get; set; }

        [Display(Name = "Номер маршрута")]
        public TSRoute? TSRoute { get; set; }

        [Display(Name = "Количество мест")]
        public int TSBusId { get; set; }

        [Display(Name = "Количество мест")]
        public TSBus? TSBus { get; set; }

        [Display(Name = "Время отбытия")]
        public string time { get; set; }
    }
}