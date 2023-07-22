﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Infrastructure.Data.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

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

        public DbSet<UserBook> UsersBooks { get; set; } = null!;

        public DbSet<ShoppingCart> ShoppingCart { get; set; } = null!;

        public DbSet<UserOrder> UsersOrders { get; set; } = null!;


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

            builder.Entity<UserBook>()
                .HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<UserOrder>()
                .HasKey(x => new { x.UserId, x.OrderId });

            builder.Entity<OrderBook>()
                .HasOne(x => x.Book)
                .WithMany(x => x.OrdersBooks)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(sc => sc.User)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ShoppingCart>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<Book>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<Book>()
                .Property(p => p.Rating)
                .HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }
    }
}