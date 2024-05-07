using BookStore.Entity.Dapper.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BookStore.Entity.Repository
{
    public class BookRepository
    {
        private readonly string connectionString;

        public BookRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStore");
        }

        public async Task<BookEntity> Get(int id)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"SELECT * FROM dbo.Books WHERE Id = @id";
            var parameters = new { id };

            // 3. Get data
            BookEntity book = await connection.QueryFirstOrDefaultAsync<BookEntity>(query, parameters);

            return book;
        }

        public async Task<IEnumerable<BookEntity>> GetByAuthorId(int authorId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            string booksQuery = "SELECT bo.* FROM dbo.Books bo JOIN dbo.Authors a ON a.Id = bo.AuthorId WHERE a.Id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", authorId);

            try
            {
                IEnumerable<BookEntity> books = await connection.QueryAsync<BookEntity>(booksQuery, parameters);
                return books;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Add(string name, string description, string genre)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"INSERT INTO Books VALUES(@name, @description, @genre)";

            // 3. Set params
            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            parameters.Add("@description", description);
            parameters.Add("@genre", genre);

            // 4. Exec query
            await connection.QueryAsync(query, parameters);
        }

        public async Task Update(int id, string name, string description, string genre)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"UPDATE Books SET Name = @name, Description = @description, Genre = @genre WHERE Id = @id";

            // 3. Set params
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@name", name);
            parameters.Add("@description", description);
            parameters.Add("@genre", genre);

            // 4. Exec query
            await connection.QueryAsync(query, parameters);
        }

        public async Task Delete(int id)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"DELETE FROM Books WHERE Id = @id";

            // 3. Set params
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            // 4. Exec query
            await connection.QueryAsync(query, parameters);
        }
    }
}
