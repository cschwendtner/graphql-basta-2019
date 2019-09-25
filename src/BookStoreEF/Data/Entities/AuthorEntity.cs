using System;
using System.Collections.Generic;

namespace BookStore.Data.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<BookEntity> Books { get; set; }
    }
}
