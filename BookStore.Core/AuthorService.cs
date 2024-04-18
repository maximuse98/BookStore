using BookStore.Core.Repository;
using BookStore.Core.Model;
using System.Globalization;

namespace BookStore.Core
{
    public class AuthorService
    {
        public readonly AuthorRepository respository;

        public AuthorService(AuthorRepository repository)
        {
            this.respository = repository;
        }

        public async Task<Author> GetAuthor(int id)
        {
            Author author = await respository.Get(id);
            return author ?? throw new Exception("No author founded");
        }

        public async Task AddAuthor(string name)
        {
            await respository.Add(name);
        }

        public async Task UpdateAuthor(int id, string name)
        {
            try
            {
                await respository.Update(id, name);
            }
            catch
            {
                Console.WriteLine("Something went wrong");
                throw;
            }
        }

        public async Task DeleteAuthor(int id)
        {
            try
            {
                await respository.Delete(id);
            }
            catch
            {
                Console.WriteLine("Something went wrong");
                throw;
            }

        }
    }
}
