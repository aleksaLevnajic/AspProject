using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class ServiceSchedule
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime ScheduledFor { get; set; }
        public int ServiceTypeId { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime? FinishedAt { get; set; }
        public decimal? Price { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
