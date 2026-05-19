using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Seeder;

namespace ProductionSystem.Infrastructure.Data.Context
{
    public class ProductionSystemDbContext : DbContext
    {
        public ProductionSystemDbContext(DbContextOptions<ProductionSystemDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductParameter> Parameters { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductParameterItem> ProductParameterItems { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductionReceipt> ProductionReceipts { get; set; }
        public DbSet<ProductionReceiptPersonnel> ProductionReceiptPersonnels { get; set; }
        public DbSet<ProductionReceiptParameter> ProductionReceiptParameters { get; set; }
        public DbSet<WasteReceipt> WasteReceipts { get; set; }
        public DbSet<WasteReceiptPersonnel> WasteReceiptPersonnels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global Soft Delete Filter
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Unit>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ProductParameter>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ParameterValue>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ProductParameterItem>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Personnel>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ProductionReceipt>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<WasteReceipt>().HasQueryFilter(e => !e.IsDeleted);

            // Cascade Delete جلوگیری
            modelBuilder.Entity<ProductionReceipt>()
                .HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductionReceipt>()
                .HasOne(p => p.Order).WithMany().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WasteReceipt>()
                .HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WasteReceipt>()
                .HasOne(p => p.Order).WithMany().HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductionReceiptParameter>()
                .HasOne(p => p.ProductionReceipt).WithMany(r => r.ProductReceiptParameters)
                .HasForeignKey(p => p.ProductionReceiptId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductionReceiptParameter>()
                .HasOne(p => p.ProductParameter).WithMany()
                .HasForeignKey(p => p.ProductParameterId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductionReceiptParameter>()
                .HasOne(p => p.ParameterValue).WithMany()
                .HasForeignKey(p => p.ParameterValueId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role).WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new RoleSeeder());
            modelBuilder.ApplyConfiguration(new UserSeeder());
        }
    }
}
