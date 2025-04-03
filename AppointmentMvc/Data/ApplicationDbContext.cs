using AppointmentMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
