using Microsoft.EntityFrameworkCore;
using pleanrioASP.Data.Entities;

namespace pleanrioASP.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Telefono>().HasIndex(t => t.Id).IsUnique();
           
        }
    }
}
