using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class RegistrationPlate
    {
        public RegistrationPlate()
        {
            Registrations = new HashSet<Registration>();
        }

        public int Id { get; set; }
        public string RegistrationPlate1 { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
