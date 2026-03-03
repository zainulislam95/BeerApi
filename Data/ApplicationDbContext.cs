using BeerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemArticle> ItemArticles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.BrandName).HasMaxLength(200);
                entity.Property(p => p.Name).HasMaxLength(200);
                entity.Property(p => p.DescriptionText).HasMaxLength(2000); 
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.BrandName).HasMaxLength(200);
                entity.Property(p => p.Name).HasMaxLength(200);
                entity.Property(p => p.DescriptionText).HasMaxLength(2000);
                entity.HasMany(p => p.Articles).WithOne().HasForeignKey(a => a.ItemId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemArticle>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.ShortDescription).HasMaxLength(500);
                entity.Property(a => a.Unit).HasMaxLength(100);
                entity.Property(a => a.Image).HasMaxLength(500);
                entity.Property(a => a.Price).HasColumnType("decimal(18,2)");
                entity.Property(a => a.PricePerUnit).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.FirstName).HasMaxLength(200).IsRequired();
                entity.Property(c => c.LastName).HasMaxLength(200).IsRequired();
                entity.Property(c => c.Email).HasMaxLength(320);
                entity.Property(c => c.Phone).HasMaxLength(50);
                entity.Property(c => c.CreatedAt);
            });

            
        }
    }
}
