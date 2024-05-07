using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entity.Dapper.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
    }
}
