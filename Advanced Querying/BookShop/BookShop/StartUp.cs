namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {

        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            List<string> books = new List<string>();
            switch (command)
            {
                case "adult":
                    books = context.Books
                        .Where(e => e.AgeRestriction == Models.Enums.AgeRestriction.Adult)
                        .OrderBy(e => e.Title).Select(e => e.Title)
                        .ToList();
                    break;
                case "teen":
                    books = context.Books
                        .Where(e => e.AgeRestriction == Models.Enums.AgeRestriction.Teen)
                        .OrderBy(e => e.Title).Select(e => e.Title)
                        .ToList();
                    break;
                case "minor":
                    books = context.Books
                        .Where(e => e.AgeRestriction == Models.Enums.AgeRestriction.Minor)
                        .OrderBy(e => e.Title).Select(e => e.Title)
                        .ToList();
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(books.ToString());

            return sb.ToString();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            goldenBooks.ForEach(b => sb.AppendLine(b.Title));
            return sb.ToString();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in booksByPrice)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }
            return sb.ToString();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            year = int.Parse(Console.ReadLine());

            var booksNotReleased = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in booksNotReleased)
            {
                sb.Append(book.Title);
            }
            return sb.ToString();
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var titles = input.ToLower().Split(
                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var getBooksByCategories = context.Books
                .Where(b => b.BookCategories.Any(category => titles.Contains(category.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var title in getBooksByCategories)
            {
                sb.AppendLine(title.Title);
            }
            return sb.ToString();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime releaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var getBooksReleasedBefore = context.Books
                .Where(b => b.ReleaseDate < releaseDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in getBooksReleasedBefore)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }
            return sb.ToString();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            input = Console.ReadLine();

            var getAuthorNamesEndingIn = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + a.LastName
                })
                .OrderBy(a => a)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var author in getAuthorNamesEndingIn)
            {
                sb.AppendLine(author.FullName);
            }
            return sb.ToString();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {

            var bookTitlesContaining = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => new
                {
                    b.Title
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in bookTitlesContaining)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var booksByAuthor = context.Books
                .Where(b => b.Title.StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    b.Author
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var author in booksByAuthor)
            {
                sb.AppendLine($"{author.Title} ({author.Author})");
            }
            return sb.ToString();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var countBooks = context.Books
                .Where(e => e.Title.Length > lengthCheck)
                .Count();

            return countBooks;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var countCopies = context.Authors
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}",
                    CoupiesSum = a.Books.Sum(c => c.Copies)
                })
                .OrderBy(a => a.CoupiesSum)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var copy in countCopies)
            {
                sb.AppendLine($"{copy.FullName} - {copy.CoupiesSum}");
            }
            return sb.ToString();
        }
    }
}
