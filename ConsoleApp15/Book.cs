using System;

namespace ConsoleApp15
{
    public class Book
    {
        private static int nextId = 1;

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public Genre Genre { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }

        public Book(string title, string author, Genre genre, int year, decimal price)
        {
            Id = nextId;
            nextId = nextId + 1;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ID: {Id} | Название: {Title} | Автор: {Author} | Жанр: {Genre} | Год: {Year} | Цена: {Price} руб.");
        }
    }
}