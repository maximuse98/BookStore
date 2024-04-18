using BookStore.Core.Model;
using BookStore.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookStore.Core
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
            Book book = await repository.Get(id);

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
