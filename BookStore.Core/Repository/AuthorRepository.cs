using BookStore.Core.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
    public class AuthorRepository
    {
        public readonly string ConnectionString;

        public AuthorRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("BookStore");
        }

        public async Task<Author> Get(int id)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT * from dbo.Authors WHERE Id = @id";

            var parameters = new DynamicParameters();

            parameters.Add("@id", id);

            Author author = await connection.QueryFirstOrDefaultAsync(query, parameters);

            return author;
        }

        public async Task Add(string name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "insert into Authors values(@Name)";

            var parameters = new DynamicParameters();

            parameters.Add("@Name", name);

            await connection.QueryFirstOrDefaultAsync(query, parameters);


        }

        public async Task Update(int id, string name)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "update Authors set Name = @name where id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@Name", name);

            await connection.QueryFirstOrDefaultAsync(query, parameters);

        }

        public async Task Delete(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "delete from Authors where id = @id";

            var parameters = new DynamicParameters();

            parameters.Add("@Id", id);

            await connection.QueryFirstOrDefaultAsync(query,parameters);
        }
    }
}
