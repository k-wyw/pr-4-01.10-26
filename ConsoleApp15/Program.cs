using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp15
{
    internal class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            books.Add(new Book("Мастер и Маргарита", "Булгаков", Genre.Роман, 1967, 850));
            books.Add(new Book("Колобок", "Пушкин", Genre.Сказка, 1949, 620));
            books.Add(new Book("Убийство в Восточном экспрессе", "Кристи", Genre.Детектив, 1934, 540));
            books.Add(new Book("Властелин колец", "Толкин", Genre.Фэнтези, 1954, 1200));
            books.Add(new Book("Красная книга", "Россия", Genre.НаучнаяЛитература, 1988, 980));

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n Библиотека");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по ID");
                Console.WriteLine("3. Найти по названию");
                Console.WriteLine("4. Найти по автору");
                Console.WriteLine("5. Найти по жанру");
                Console.WriteLine("6. Сортировать по названию");
                Console.WriteLine("7. Сортировать по году");
                Console.WriteLine("8. Самая дорогая и дешёвая");
                Console.WriteLine("9. Группировка по авторам");
                Console.WriteLine("10. Показать все книги");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": DeleteBook(); break;
                    case "3": FindByTitle(); break;
                    case "4": FindByAuthor(); break;
                    case "5": FindByGenre(); break;
                    case "6": SortByTitle(); break;
                    case "7": SortByYear(); break;
                    case "8": ShowExpensiveAndCheap(); break;
                    case "9": GroupByAuthor(); break;
                    case "10": ShowAll(); break;
                    case "0": running = false; Console.WriteLine("Выход"); break;
                    default: Console.WriteLine("Неверный пункт."); break;
                }
            }
        }

  
        static void AddBook()
        {
            Console.WriteLine("\n Добавление книги ");

            string title = "";
            while (title == "")
            {
                Console.Write("Название: ");
                title = Console.ReadLine();
                if (title == "") Console.WriteLine("Название не может быть пустым.");
            }

            string author = "";
            while (author == "")
            {
                Console.Write("Автор: ");
                author = Console.ReadLine();
                if (author == "") Console.WriteLine("Автор не может быть пустым.");
            }

            Console.WriteLine("Жанры: 1 - Роман, 2 - Сказка, 3 - Детектив, 4 - Фэнтези, 5 - Научная литература");
            Genre genre = Genre.Роман;
            bool genreOk = false;
            while (genreOk == false)
            {
                Console.Write("Жанр (1-5): ");
                string g = Console.ReadLine();
                if (g == "1") { genre = Genre.Роман; genreOk = true; }
                else if (g == "2") { genre = Genre.Сказка; genreOk = true; }
                else if (g == "3") { genre = Genre.Детектив; genreOk = true; }
                else if (g == "4") { genre = Genre.Фэнтези; genreOk = true; }
                else if (g == "5") { genre = Genre.НаучнаяЛитература; genreOk = true; }
                else Console.WriteLine("Неверный жанр.");
            }

            int year = 0;
            while (year <= 0)
            {
                Console.Write("Год издания: ");
                if (int.TryParse(Console.ReadLine(), out year) == false)
                {
                    Console.WriteLine("Год должен быть числом.");
                    year = 0;
                }
                else if (year <= 0 || year > DateTime.Now.Year)
                {
                    Console.WriteLine("Год должен быть положительным и не больше текущего.");
                    year = 0;
                }
            }

            decimal price = 0;
            while (price <= 0)
            {
                Console.Write("Цена: ");
                if (decimal.TryParse(Console.ReadLine(), out price) == false)
                {
                    Console.WriteLine("Цена должна быть числом.");
                    price = 0;
                }
                else if (price <= 0)
                {
                    Console.WriteLine("Цена должна быть больше нуля.");
                }
            }

            books.Add(new Book(title, author, genre, year, price));
            Console.WriteLine("Книга добавлена.");
        }

        static void DeleteBook()
        {
            Console.WriteLine("\n Удаление книги ");
            Console.Write("Введите ID: ");

            int id;
            if (int.TryParse(Console.ReadLine(), out id) == false)
            {
                Console.WriteLine("ID должен быть числом.");
                return;
            }

            Book found = null;
            foreach (Book b in books)
            {
                if (b.Id == id)
                {
                    found = b;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("Книга с таким ID не найдена.");
                return;
            }

            books.Remove(found);
            Console.WriteLine("Книга удалена.");
        }

     
        static void FindByTitle()
        {
            Console.WriteLine("\n Поиск по названию ");
            Console.Write("Введите часть названия: ");
            string query = Console.ReadLine().ToLower();

            List<Book> found = new List<Book>();
            foreach (Book b in books)
            {
                if (b.Title.ToLower().Contains(query))
                    found.Add(b);
            }

            if (found.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }

            foreach (Book b in found)
                b.PrintInfo();
        }

        static void FindByAuthor()
        {
            Console.WriteLine("\n Поиск по автору ");
            Console.Write("Введите часть имени автора: ");
            string query = Console.ReadLine().ToLower();

            List<Book> found = new List<Book>();
            foreach (Book b in books)
            {
                if (b.Author.ToLower().Contains(query))
                    found.Add(b);
            }

            if (found.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }

            foreach (Book b in found)
                b.PrintInfo();
        }

   
        static void FindByGenre()
        {
            Console.WriteLine("\n Поиск по жанру ");
            Console.WriteLine("1 - Роман, 2 - Сказка, 3 - Детектив, 4 - Фэнтези, 5 - Научная литература");
            Console.Write("Жанр (1-5): ");

            string g = Console.ReadLine();
            Genre genre;
            if (g == "1") genre = Genre.Роман;
            else if (g == "2") genre = Genre.Сказка;
            else if (g == "3") genre = Genre.Детектив;
            else if (g == "4") genre = Genre.Фэнтези;
            else if (g == "5") genre = Genre.НаучнаяЛитература;
            else { Console.WriteLine("Неверный жанр."); return; }

            List<Book> found = new List<Book>();
            foreach (Book b in books)
            {
                if (b.Genre == genre)
                    found.Add(b);
            }

            if (found.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }

            foreach (Book b in found)
                b.PrintInfo();
        }

        static void SortByTitle()
        {
            Console.WriteLine("\n Сортировка по названию ");
            var sorted = books.OrderBy(b => b.Title).ToList();
            foreach (Book b in sorted)
                b.PrintInfo();
        }

        static void SortByYear()
        {
            Console.WriteLine("\n Сортировка по году ");
            var sorted = books.OrderBy(b => b.Year).ToList();
            foreach (Book b in sorted)
                b.PrintInfo();
        }

        static void ShowExpensiveAndCheap()
        {
            Console.WriteLine("\n Самая дорогая и дешёвая ");

            if (books.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            var expensive = books.OrderByDescending(b => b.Price).First();
            var cheap = books.OrderBy(b => b.Price).First();

            Console.WriteLine("Самая дорогая:");
            expensive.PrintInfo();
            Console.WriteLine("Самая дешёвая:");
            cheap.PrintInfo();
        }

        static void GroupByAuthor()
        {
            Console.WriteLine("\n Группировка по авторам ");

            var groups = books
                .GroupBy(b => b.Author)
                .Select(g => new { Author = g.Key, Count = g.Count() })
                .ToList();

            foreach (var item in groups)
                Console.WriteLine($"{item.Author}: {item.Count}");
        }

        static void ShowAll()
        {
            Console.WriteLine("\n Все книги ");

            if (books.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (Book b in books)
                b.PrintInfo();
        }
    }
}