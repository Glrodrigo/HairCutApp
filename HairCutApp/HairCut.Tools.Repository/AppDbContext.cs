using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserBase> Users { get; set; }
        public DbSet<AccessBase> Access { get; set; }
        public DbSet<CategoryBase> Categories { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBase>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<UserBase>()
                .Property(a => a.Email)
                .IsRequired();

            // Another model
            modelBuilder.Entity<AccessBase>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<AccessBase>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<AccessBase>()
                .Property(b => b.AccountName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<AccessBase>()
                .Property(b => b.ProfileName)
                .IsRequired()
                .HasMaxLength(200);

            // Another model
            modelBuilder.Entity<CategoryBase>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CategoryBase>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<CategoryBase>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            base.OnModelCreating(modelBuilder);
        }
    }
}
