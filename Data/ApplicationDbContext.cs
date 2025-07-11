using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Local_WRP.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<TenantDetail> TenantDetails { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserWorkOrders> UserWorkOrders { get; set; }
    }
}
