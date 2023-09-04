using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Registrations = new HashSet<Registration>();
            ServiceSchedules = new HashSet<ServiceSchedule>();
        }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Label { get; set; }

        public virtual Model Model { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; }
    }
}
