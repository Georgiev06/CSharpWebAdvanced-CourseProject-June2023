using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Infrastructure.Common.EntityValidationConstants
{
    public class EntityValidationConstants
    {
        public class Book
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 5;

            public const int AuthorNameMaxLength = 50;
            public const int AuthorNameMinLength = 3;

            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 10;

            public const int ImageUrlMaxLength = 2048;
            public const int ImageUrlMinLength = 20;
        }

        public class Category
        {
            public const int NameMaxLength = 30;
            public const int NameMinLength = 3;
        }

        public class Review
        {
            public const int RatingMaxLength = 10;
            public const int RatingMinLength = 1;

            public const int CommentMaxLength = 200;
            public const int CommentMinLength = 15;
        }

        public class User
        {
            public const int FirstNameMaxLength = 20;
            public const int FirstNameMinLength = 3;

            public const int LastNameMaxLength = 15;
            public const int LastNameMinLength = 4;

            public const int AddressMaxLength = 95;
            public const int AddressMinLength = 4;
        }
    }
}
