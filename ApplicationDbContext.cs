using Microsoft.EntityFrameworkCore;

namespace LeavePortal.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
