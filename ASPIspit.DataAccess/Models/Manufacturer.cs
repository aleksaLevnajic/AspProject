using System;
using System.Collections.Generic;

#nullable disable

namespace ASPIspit.DataAccess.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            ManufacturersVehicleTypes = new HashSet<ManufacturersVehicleType>();
            Models = new HashSet<Model>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ManufacturersVehicleType> ManufacturersVehicleTypes { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
