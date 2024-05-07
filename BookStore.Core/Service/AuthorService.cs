using BookStore.Entity.Repository;
using BookStore.Core.Model;
using BookStore.Entity.Dapper.Entities;
using BookStore.Entity.EFCore;

namespace BookStore.Core.Service
{
    public class AuthorService
    {
        private readonly AuthorRepository respository;
        private readonly BookRepository bookRepository;

        private BookStoreContext context;

        public AuthorService(AuthorRepository repository, BookRepository bookRepository)
        {
            respository = repository;
            this.bookRepository = bookRepository;

            context = new BookStoreContext();
        }

        public async Task<IEnumerable<Model.Author>> GetAllAuthors()
        {
            //IEnumerable<AuthorEntity> authors = await respository.GetAll();

            Microsoft.EntityFrameworkCore.DbSet<Entity.EFCore.Author> authors = context.Authors;

            List<Model.Author> result = new List<Model.Author>();

            foreach (var author in authors) 
            { 
                result.Add(new Model.Author()
                {
                    Id = author.Id,
                    Name = author.Name,
                    Description = author.Description
                });
            }

            return result;
        }

        public async Task<Model.Author> GetAuthor(int id)
        {
            //AuthorEntity author = await respository.Get(id);
            //IEnumerable<BookEntity> enumerable = await bookRepository.GetByAuthorId(id);

            Entity.EFCore.Author author = context.Authors.Where(x => x.Id == id).FirstOrDefault();
            Entity.EFCore.Book[] books = context.Books.Where(x => x.Author.Id == id).ToArray();

            var result = new Model.Author()
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Books = new List<Model.Book>()
            };

            foreach (var book in books)
            {
                result.Books.Add(new Model.Book()
                {
                    AuthorId = id,
                    Name = book.Name,
                    Description = book.Description,
                    Id = book.Id,
                    Genre = book.Genre
                });
            }

            return result ?? throw new Exception("No author founded");
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
