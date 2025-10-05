using Microsoft.EntityFrameworkCore;
using WebApplicationBaseRepo.Models;

namespace WebApplicationBaseRepo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext() : base() { }
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }
    }
}
