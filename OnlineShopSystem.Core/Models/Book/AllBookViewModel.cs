﻿namespace OnlineShopSystem.Core.Models.Book
{
    public class AllBookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Rating { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string UserId { get; set; }
    }
}
