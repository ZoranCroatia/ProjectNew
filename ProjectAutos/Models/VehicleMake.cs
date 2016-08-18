using System.Collections.Generic;

namespace ProjectAutos.Models
{
    public class VehicleMake
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
