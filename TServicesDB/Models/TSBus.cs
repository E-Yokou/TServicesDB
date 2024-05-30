using System.ComponentModel.DataAnnotations;

namespace TServicesDB.Models
{
    public class TSBus
    {
        public int TSBusID { get; set; }

        [Display(Name = "Тип автобуса")]
        public string type_bus { get; set; }

        [Display(Name = "Модель автобуса")]
        public string brand { get; set; }

        [Display(Name = "Гос.номера автобуса")]
        public string goverment_number { get; set; }

        [Display(Name = "Количество мест в автобусе")]
        public int place { get; set; }

        [Display(Name = "Номер Маршрута")]
        public int? TSRouteId { get; set; }

        [Display(Name = "Номер Маршрута")]
        public TSRoute? TSRoute { get; set; }

        public ICollection<TSDriver> TSDriver { get; set; }

        public TSBus()
        {
            TSDriver = new List<TSDriver>();
        }
    }
}
