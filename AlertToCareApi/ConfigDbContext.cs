using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareApi
{
    public class ConfigDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PatientDatabase.db");
        }
        public DbSet<ICURooms> IcuRoom { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patient { get; set; }
        public DbSet<PatientVitals> PatientVital { get; set; }

    }
    
}
