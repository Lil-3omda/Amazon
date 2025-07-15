using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Amazon_API.Models.Entities.User;
using Amazon_API.Models.Entities.Carting;
using Amazon_API.Models.Entities.Categories;
using Amazon_API.Models.Entities.Ordering;
using Amazon_API.Models.Entities.Products;
using Amazon_API.Models.Entities.Reviews;

namespace Amazon_API.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishListItem> WishlistItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Review>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<CartItem>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<WishListItem>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Order>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<OrderItem>().HasQueryFilter(e => !e.IsDeleted);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Review>()
               .HasOne(r => r.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
               .HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProductImage>()
              .HasOne(i => i.Product)
              .WithMany(p => p.Images)
              .HasForeignKey(i => i.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.NoAction); 


            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<WishListItem>()
               .HasOne(wi => wi.User)
               .WithMany(u => u.wishListItems)
               .HasForeignKey(wi => wi.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<WishListItem>()
                .HasOne(wi => wi.Product)
                .WithMany(p => p.WishListItems)
                .HasForeignKey(wi => wi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
               .Property(p => p.Price)
               .HasPrecision(18, 2); 

            builder.Entity<Order>()
                   .Property(o => o.TotalPrice)
                   .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                   .Property(oi => oi.UnitPrice)
                   .HasPrecision(18, 2);
        }
    }
}
