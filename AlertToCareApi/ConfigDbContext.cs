using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareApi
{
    public class ConfigDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=CaseStudy2Database.db");
        }
        public DbSet<Icu> Icu { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<VitalsLogs> VitalsLogs { get; set; }
    }
    
}
