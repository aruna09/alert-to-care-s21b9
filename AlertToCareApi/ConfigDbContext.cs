using AlertToCareApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareApi
{
    public class ConfigDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=D:\a\assist-purchase-s21b8\assist-purchase-s21b8\AlertToCareApi\CaseStudy2Database.db");
        }
        public DbSet<Icu> Icu { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<VitalsLogs> VitalsLogs { get; set; }
    }
    
}
