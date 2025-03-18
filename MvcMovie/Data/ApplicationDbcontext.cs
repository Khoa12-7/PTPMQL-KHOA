using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }  // Sửa Employeee -> Employees
        public DbSet<HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;
        public DbSet<DaiLy> DaiLy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình phân loại TPH (Table-Per-Hierarchy)
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("PersonType")
                .HasValue<Person>("Person")
                .HasValue<Employee>("Employee");

            // Cấu hình liên kết DaiLy ↔ HeThongPhanPhoi
            modelBuilder.Entity<DaiLy>()
                .HasOne(d => d.HeThongPhanPhoi)
                .WithMany()
                .HasForeignKey(d => d.MaHTPP)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
