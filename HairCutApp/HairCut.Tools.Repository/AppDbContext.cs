using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserBase> Users { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBase>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<UserBase>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<UserBase>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<UserBase>()
                .Property(t => t.Email)
                .IsRequired();
        }
    }
}
