using Microsoft.EntityFrameworkCore;
using SprintManagement.Core.Entities;

namespace SprintManagement.Infrastructure.Data
{
    public class SprintContext : DbContext
    {
        public SprintContext(DbContextOptions<SprintContext> options) : base(options)
        {
        }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<SprintDetail> SprintDetails { get; set; }
    }
}
