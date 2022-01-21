using Many2Many.Entities;
using Microsoft.EntityFrameworkCore;

namespace Many2Many
{
    public class MyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=MyContext;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(p => p.Courses)
                .WithMany(p => p.Students)
                .UsingEntity<CourseSubscription>(
                    j => j
                        .HasOne(pt => pt.Course)
                        .WithMany(t => t.CourseSubscriptions)
                        .HasForeignKey(pt => pt.CourseId),
                    j => j
                        .HasOne(pt => pt.Student)
                        .WithMany(p => p.CourseSubscriptions)
                        .HasForeignKey(pt => pt.StudentId),
                    j =>
                    {
                        j.Property(pt => pt.SubscriptionDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.CourseId, t.StudentId });
                    });

            var courses = new List<Course>
            {
                new() {Id = 1, Name = ".NET 6"},
                new() {Id = 2, Name = "Angular"},
                new() {Id = 3, Name = "Xamarin / .NET MAUI"},
                new() {Id = 4, Name = "Javascript"}
            };

            modelBuilder.Entity<Course>().HasData(courses);
        }
    }
}
