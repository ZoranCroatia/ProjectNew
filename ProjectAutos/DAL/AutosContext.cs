using ProjectAutos.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjectAutos.DAL
{
    public class AutosContext : DbContext
    {

        public AutosContext() : base("AutosContext")
        {
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}