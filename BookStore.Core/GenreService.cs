using BookStore.Core.Model;
using BookStore.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core
{
    public class GenreService
    {
        private readonly GenreRepository repository;

        public GenreService(GenreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await repository.GetAll();
        }

        public async Task<Genre> Get(int Id)
        {
            return await repository.Get(Id);
        }

    }
}
