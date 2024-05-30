using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TServicesDB.Models
{
    public class TSRoute
    {
        public int TSRouteID { get; set; }

        [Display(Name = "Номер Маршрута")]
        public string numberRoute { get; set; }

        [Display(Name = "Отправление (город)")]
        public string firstCity { get; set; }

        [Display(Name = "Прибытие (город)")]
        public string lastCity { get; set; }

        [Display(Name = "Время отправления")]
        public string time { get; set; }

        public ICollection<TSStopover> TSStopovers { get; set; }
        public ICollection<TSBus> TSBus { get; set; }

        public TSRoute()
        {
            TSStopovers = new List<TSStopover>();
            TSBus = new List<TSBus>();
        }
    }
}
