using BookStore.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
    public class GenreRepository
    {
        private readonly string ConnectionString;

        public GenreRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("BookStore");
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            using var sqlconnection = new SqlConnection(ConnectionString);
            string query = "SELECT * FROM dbo.Genres";

            return await sqlconnection.QueryAsync<Genre>(query);
        }

        public async Task<Genre> Get(int Id)
        {
            using var sqlconnection = new SqlConnection(ConnectionString);
            string query = "SELECT Name from dbo.Genres WHERE Id = @id";
            string booksQuery = "SELECT * FROM dbo.Books WHERE GenreId = @id";

            var parameters = new DynamicParameters();

            parameters.Add("@id", Id);

            try
            {
                Genre genre = await sqlconnection.QueryFirstAsync<Genre>(query, parameters);
                genre.Books = await sqlconnection.QueryAsync<Book>(booksQuery, parameters);

                return genre;
            }
            catch(Exception ex) 
            {
                throw;
            }



        }
    }
}
