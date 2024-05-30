using System.ComponentModel.DataAnnotations;

namespace TServicesDB.Models
{
    public class TSDriver
    {
        public int TSDriverID { get; set; }

        [Display(Name = "Имя")]
        public string name { get; set; }

        [Display(Name = "Фамилия")]
        public string middleName { get; set; }

        [Display(Name = "Отчество")]
        public string lastName { get; set; }

        [Display(Name = "Управляемый Автобус")]
        public int? TSBusId { get; set; }

        [Display(Name = "Управляемый Автобус")]
        public TSBus? TSBus { get; set; }

        [Display(Name = "Фотография")]
        public string? Photo { get; set; }
    }
}
