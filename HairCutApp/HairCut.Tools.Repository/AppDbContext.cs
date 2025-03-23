using HairCut.Tools.Domain;
using Microsoft.EntityFrameworkCore;

namespace HairCut.Tools.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserBase> Users { get; set; }
        public DbSet<AccessBase> Access { get; set; }
        public DbSet<CategoryBase> Categories { get; set; }
        public DbSet<ProductBase> Products { get; set; }
        public DbSet<BucketBase> Buckets { get; set; }
        public DbSet<ItemBase> Items { get; set; }
        public DbSet<OrderBase> Orders { get; set; }
        public DbSet<InvoiceBase> Invoices { get; set; }


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


            modelBuilder.Entity<ProductBase>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ProductBase>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<ProductBase>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ProductBase>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<ProductBase>()
                .Property(c => c.Price)
                .IsRequired();

            modelBuilder.Entity<ProductBase>()
                .Property(c => c.Total)
                .IsRequired();


            modelBuilder.Entity<BucketBase>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<BucketBase>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<BucketBase>()
                .Property(d => d.CreateUserId)
                .IsRequired();

            modelBuilder.Entity<BucketBase>()
                .Property(d => d.ImageId)
                .IsRequired();


            modelBuilder.Entity<ItemBase>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<ItemBase>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<ItemBase>()
                .Property(e => e.Quantity)
                .IsRequired();


            modelBuilder.Entity<OrderBase>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<OrderBase>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<OrderBase>()
                .Property(f => f.UserId)
                .IsRequired();

            modelBuilder.Entity<OrderBase>()
                .Property(f => f.Status)
                .IsRequired();


            modelBuilder.Entity<InvoiceBase>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<InvoiceBase>()
                .Property(g => g.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<InvoiceBase>()
                .Property(g => g.UserId)
                .IsRequired();

            modelBuilder.Entity<InvoiceBase>()
                .Property(g => g.Status)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
