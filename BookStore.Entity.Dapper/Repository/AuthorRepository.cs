using BookStore.Entity.Dapper.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entity.Repository
{
    public class AuthorRepository
    {
        public readonly string ConnectionString;

        public AuthorRepository(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("BookStore");
        }

        public async Task<IEnumerable<AuthorEntity>> GetAll()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Id, Name, Description FROM dbo.Authors";

            var parameters = new DynamicParameters();

            return await connection.QueryAsync<AuthorEntity>(query, parameters);
        }

        public async Task<AuthorEntity> Get(int id)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            string query = "SELECT Name, Description from dbo.Authors WHERE Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            try
            {
                AuthorEntity author = await connection.QueryFirstAsync<AuthorEntity>(query, parameters);
                return author;
            }
            catch (Exception ex) 
            {
                throw;    
            }
        }

        public async Task Add(string name)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

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
