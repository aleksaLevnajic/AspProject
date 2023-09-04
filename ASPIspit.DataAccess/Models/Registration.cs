using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class Registration
    {
        public int Id { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime IssuedAt { get; set; }
        public int RegistrationPlateId { get; set; }
        public int VehicleId { get; set; }

        public virtual RegistrationPlate RegistrationPlate { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
