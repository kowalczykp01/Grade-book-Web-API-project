using Microsoft.EntityFrameworkCore;

namespace Grade_Book_API.Entities
{
    public class GradeBookDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=GradeBookDb;Trusted_Connection=True;";
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<FinalGrade> FinalGrades { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(s => s.Name)
                .IsRequired();
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Student>()
               .Property(s => s.Surname)
               .IsRequired()
               .HasMaxLength(25);
            modelBuilder.Entity<Subject>()
               .Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(35);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
