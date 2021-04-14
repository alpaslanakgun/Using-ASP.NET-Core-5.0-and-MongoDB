using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Core
{
    public class BookServices : IBookServices
    {
        private readonly IMongoCollection<Book> _books;
        public BookServices(IDbClient dbClient)
        {
            _books=dbClient.GetBookCollection();

        }
        public Book AddBook(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

   
        public Book GetBook(string id) => _books.Find(book => book.Id == id).First();
       

        public List<Book> GetBooks() => _books.Find(book => true).ToList();
        public void DeleteBook(string id)
        {
            _books.DeleteOne(book => book.Id == id);
            
            
        }
        public Book UpdateBook(Book book)
        {
            GetBook(book.Id);
            _books.ReplaceOne(b => b.Id ==book.Id,book);
            return book;
        }

       
    }
}
