namespace ProjectAutos.Models
{
    public class VehicleModel
    {
        public int VehicleModelID { get; set; }
        public int VehicleMakeID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}