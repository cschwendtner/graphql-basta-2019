using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Data.EF
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        public void SeedData()
        {
            Database.EnsureCreated();

            if (!Authors.Any() && !Books.Any())
            {
                var authors = new List<AuthorEntity>();
                for (var authorCnt = 0; authorCnt < 5; authorCnt++)
                {
                    authors.Add(new AuthorEntity { FirstName = $"Firstname {authorCnt + 1}", LastName = $"Lastname {authorCnt + 1}" });
                };

                Authors.AddRange(authors);
                SaveChanges();

                var books = new List<BookEntity>();
                for (var bookCnt = 0; bookCnt < 20; bookCnt++)
                {
                    Books.Add(new BookEntity { Title = $"Book {bookCnt + 1}", AuthorId = authors[bookCnt % authors.Count].Id });
                    SaveChanges();
                }
            }
        }
    }
}
