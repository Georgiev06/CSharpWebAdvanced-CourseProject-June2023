using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Infrastructure.Data.Models;
using System.Reflection.Emit;

namespace OnlineShopSystem.Web.Data
{
    public class BookShopDbContext : IdentityDbContext<User>
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderBook> OrdersBooks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
                builder.Entity<Category>()
                .HasData(new Category()
                {
                    Id = 1,
                    Name = "Fantasy"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Romance"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Westerns"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Thriller"
                });

            builder.Entity<OrderBook>()
                .HasKey(x => new { x.BookId, x.OrderId });

            builder.Entity<OrderBook>()
                .HasOne(x => x.Book)
                .WithMany(x => x.OrdersBooks)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}