namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //string command = Console.ReadLine();

                //int year = int.Parse(Console.ReadLine());

                //int length = int.Parse(Console.ReadLine());

                //string resultEx1 = GetBooksByAgeRestriction(db, command);

                //string resultEx2 = GetGoldenBooks(db);

                //string resultEx3 = GetBooksByPrice(db);

                //string resultEx4 = GetBooksNotRealeasedIn(db, year);

                //string resultEx5 = GetBooksByCategory( db, command);

                //string resultEx6 = GetBooksReleasedBefore(db, command);

                //string resultEx7 = GetAuthorNamesEndingIn(db, command);

                //string resultEx8 = GetBookTitlesContaining(db, command);

                //string resultEx9 = GetBooksByAuthor(db, command);

                //int resultEx10 = CountBooks(db, length);

                //string resultEx11 = CountCopiesByAuthor(db);

                //string resultEx12 = GetTotalProfitByCategory(db);

                //string resultEx13 = GetMostRecentBooks(db);

                //IncreasePrices(db);

               int resultEx15 = RemoveBooks(db);

                Console.WriteLine($"{resultEx15} books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var deletedBooks = db.Books.Where(x => x.Copies < 4200).ToList();

            var bookCount = deletedBooks.Count;

            db.RemoveRange(deletedBooks);

            db.SaveChanges();

            return bookCount;
        }

        public static void IncreasePrices(BookShopContext db)
        {
            db.Books.Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(x => x.Price += 5);

            db.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var books = db.Categories
                .Select(x => new
                {
                    cName = x.Name,
                    bookCount = x.CategoryBooks.Sum(bc => bc.BookId),
                    books = x.CategoryBooks.Select(bc => bc.Book).OrderByDescending(z => z.ReleaseDate).Take(3)
                }).OrderBy(x => x.cName);

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
               sb.AppendLine($"--{book.cName}");
                foreach (var b in book.books)
                {
                    sb.AppendLine($"{b.Title} ({b.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var profitByCategory = db.Categories.Include(x => x.CategoryBooks).ThenInclude(x => x.Book).Select(x => new
            {
                CategoryName = x.Name,
                Profit = x.CategoryBooks.Sum(p => (p.Book.Copies * p.Book.Price))
            }).OrderByDescending(x => x.Profit).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var line in profitByCategory)
            {
                sb.AppendLine($"{line.CategoryName} ${line.Profit}");
            }

            return sb.ToString().Trim();
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var booksCopies = db.Authors.Include(x => x.Books).Select(x => new
            {
                AuthorName = string.Concat(x.FirstName, " ", x.LastName),
                NumbersOfCopies = x.Books.Sum(a => a.Copies)
            }).OrderByDescending(x => x.NumbersOfCopies).ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var line in booksCopies)
            {
                sb.AppendLine($"{line.AuthorName} - {line.NumbersOfCopies}");
            }

            return sb.ToString().Trim();
        }

        public static int CountBooks(BookShopContext db, int length)
        {
            int booksCount = db.Books.Where(x => x.Title.Length > length).Count();

            return booksCount;
        }

        public static string GetBooksByAuthor(BookShopContext db, string command)
        {
           string[] books = db.Books.Where(x => x.Author.LastName.ToLower()
            .StartsWith(command.ToLower()))
            .OrderBy(x => x.BookId).Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})")
            .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var line in books)
            {
                sb.AppendLine(line);
            }

            return sb.ToString().Trim();
        }

        public static string GetBookTitlesContaining(BookShopContext db, string command)
        {
            string[] books = db.Books.Where(x => x.Title.ToLower()
            .Contains(command.ToLower()))
            .Select(x => x.Title).OrderBy(x => x)
            .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var line in books)
            {
                sb.AppendLine(line);
            }

            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string command)
        {
            string[] authors = db.Authors
                .Where(x => x.FirstName.EndsWith(command))
                .Select(x => $"{x.FirstName} {x.LastName}")
                .OrderBy(x => x)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine(author);
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string input)
        {
            DateTime checkDate = DateTime.ParseExact(input, "dd-MM-yyyy", null);
            string[] books = context.Books
                .Where(x => x.ReleaseDate < checkDate)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}")
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString().Trim();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower().Split();

            var books = context.Books.Include(x => x.BookCategories)
                .ThenInclude(x => x.Category)
                .Where(x => x.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var line in books)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();

        }

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            string[] books = db.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
                .ToArray();

            string result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var booksByPrice = db.Books
                .Where(x => x.Price > 40)
                .OrderByDescending(x => x.Price)
                .Select(x => new { name = x.Title, price = x.Price })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in booksByPrice)
            {
                sb.AppendLine($"{book.name} - ${book.price:f2}");
            }

            return sb.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext db)
        {
            int enumValue = 2;

            string[] goldenBooks = db.Books.Where(x => x.EditionType == (EditionType)enumValue && x.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title).ToArray();

            string result = string.Join(Environment.NewLine, goldenBooks);

            return result;
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int enumValue = 0;

            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
                default:
                    break;
            }

            string[] titlies = context.Books
                            .Where(x => x.AgeRestriction == (AgeRestriction)enumValue)
                            .Select(x => x.Title).OrderBy(x => x).ToArray();
                            
            string result = string.Join(Environment.NewLine, titlies);

            return result;
        }
    }
}
