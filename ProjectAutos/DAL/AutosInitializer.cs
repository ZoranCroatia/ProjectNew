using System.Collections.Generic;
using ProjectAutos.Models;

namespace ProjectAutos.DAL
{
    public class AutosInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AutosContext>
    {
        protected override void Seed(AutosContext context)
        {
            var v = new List<VehicleMake>
            {
            new VehicleMake{Name="BMW",Abrv="Great"},
            new VehicleMake{Name="Ford",Abrv="Good"},
            new VehicleMake{Name="Volkswagen",Abrv="Cheater"},
            new VehicleMake{Name="Toyota",Abrv="Expensive"},
            new VehicleMake{Name="Peugeot",Abrv="Ok"},
            };

            v.ForEach(s => context.VehicleMakes.Add(s));
            context.SaveChanges();

            var e = new List<VehicleModel>
            {
            new VehicleModel{VehicleMakeID=1,Name="M4",Abrv="Great"},
            new VehicleModel{VehicleMakeID=1,Name="Luxury",Abrv="Good"},
            new VehicleModel{VehicleMakeID=1,Name="X5",Abrv="Good"},
            new VehicleModel{VehicleMakeID=2,Name="Mustang",Abrv="Fast"},
            new VehicleModel{VehicleMakeID=2,Name="Focus",Abrv="Good"},
            new VehicleModel{VehicleMakeID=2,Name="Ranger",Abrv="Huge"},
            new VehicleModel{VehicleMakeID=3,Name="Polo",Abrv="Good"},
            new VehicleModel{VehicleMakeID=3,Name="Buba",Abrv="Small"},
            new VehicleModel{VehicleMakeID=3,Name="Passat",Abrv="Big"},
            new VehicleModel{VehicleMakeID=4,Name="Camry",Abrv="Ok"},
            new VehicleModel{VehicleMakeID=4,Name="Tacoma",Abrv="Big"},
            new VehicleModel{VehicleMakeID=5,Name="Saloon",Abrv="Big"},

            };
            e.ForEach(s => context.VehicleModels.Add(s));
            context.SaveChanges();
        }
    }
}