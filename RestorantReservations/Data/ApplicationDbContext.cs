using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestorantReservations.Models;

namespace RestorantReservations.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RestorantReservations.Models.Table>? Table { get; set; }
        public DbSet<RestorantReservations.Models.Reservation>? Reservation { get; set; }
    }
}