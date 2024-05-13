using BookStore.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BookStore.Core.Repository
{
    public class BookRepository
    {
        private readonly string connectionString;

        public BookRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStore");
        }

        public async Task<Book> Get(int id)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"SELECT * FROM dbo.Books WHERE Id = @id";
            var parameters = new { id };

            // 3. Get data
            Book book = await connection.QueryFirstOrDefaultAsync<Book>(query, parameters);

            return book;
        }

        public async Task<IEnumerable<Book>> GetByGenre(int genreId)
        {
            using var connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM dbo.Books WHERE GenreId = @genreId";
            var parameters = new { genreId };

            return await connection.QueryAsync<Book>(query, parameters);
        }

        public async Task Add(string name, string description, int genreId)
        {
            // 1. Initiate connection
            using var connection = new SqlConnection(connectionString);

            // 2. Prepare query
            string query = $"INSERT INTO Books VALUES(@name, @description, @genre)";

            // 3. Set params
            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            parameters.Add("@description", description);
            parameters.Add("@genre", genreId);

            // 4. Exec query
            await connection.QueryAsync(query, parameters);
        }

        public async Task Update(int id, string name, string description, int genreId)
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
            parameters.Add("@genre", genreId);

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
