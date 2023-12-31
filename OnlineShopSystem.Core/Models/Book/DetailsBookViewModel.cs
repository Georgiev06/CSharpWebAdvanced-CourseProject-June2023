﻿using OnlineShopSystem.Core.Models.Review;

namespace OnlineShopSystem.Core.Models.Book
{
    public class DetailsBookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Rating { get; set; } = null!;

        public string Category { get; set; } = null!;

        public virtual IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
