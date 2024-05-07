using BookStore.Core.Model;
using BookStore.Entity.Dapper.Entities;
using BookStore.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookStore.Core.Service
{
    public class BookService
    {
        private readonly BookRepository repository;

        public BookService(BookRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateBook(string name, string description, string genre)
        {
            await repository.Add(name, description, genre);
        }

        public async Task<Book> GetBook(int id)
        {
            BookEntity bookEntity = await repository.Get(id);

            Book book = new Book()
            {
                Id = bookEntity.Id,
                Name = bookEntity.Name,
                Description = bookEntity.Description,
                Genre = bookEntity.Genre,
                AuthorId = bookEntity.AuthorId
            };

            //if (book == null)
            //throw new Exception("No book founded");

            return book ?? throw new Exception("No book founded");
            //return book == null ? book : throw new Exception("No book founded");
        }

        public async Task UpdateBook(int id, string name, string description, string genre)
        {
            try
            {
                await repository.Update(id, name, description, genre);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Update failed");
            }
        }

        public async Task DeleteBook(int id)
        {
            try
            {
                await repository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Delete failed");
            }
        }
    }
}
