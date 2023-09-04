using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPIspit.Application.DTO
{
    public class ServiceScheduleDto
    {
        public int Id { get; set; }
        public string ServiceTypeName { get; set; }
        public string RegistrationPage { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public DateTime ScheduleFor { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string AdditionalInfo { get; set; }
        public decimal? Price { get; set; }
    }
}
