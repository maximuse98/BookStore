﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Core.Model
{
    public class Author
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        public List<Book> Books { get; set;}
    }
}
