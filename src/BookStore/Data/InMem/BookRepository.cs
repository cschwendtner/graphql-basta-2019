using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BookStore.Data.InMem
{
    public class BookRepository : IBookRepository
    {
        private static readonly IList<BookEntity> books = new List<BookEntity>
        {
            new BookEntity { Id = 11, Title = "Book 11", AuthorId = 21 }, 
            new BookEntity { Id = 12, Title = "Book 12", AuthorId = 21 }, 
            new BookEntity { Id = 13, Title = "Book 13", AuthorId = 22 }, 
            new BookEntity { Id = 14, Title = "Book 14", AuthorId = 23 },
            new BookEntity { Id = 15, Title = "Book 15", AuthorId = 23 },
            new BookEntity { Id = 16, Title = "Book 15", AuthorId = 23 },
        };


        private readonly ILogger<BookRepository> logger;

        public BookRepository(ILogger<BookRepository> logger = null)
        {
            this.logger = logger;
        }

        public Task<IList<BookEntity>> GetBooksAsync()
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetBooksAsync)}");
            }
            return Task.FromResult(books);
        }

        public Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetBookByIdAsync)}");
            }
            return Task.FromResult(books.FirstOrDefault(b => b.Id == bookId));
        }

        public Task<IList<BookEntity>> GetBooksByAuthorIdAsync(int authorId)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetBooksByAuthorIdAsync)}");
            }

            IList<BookEntity> result = books.Where(b => b.AuthorId == authorId).ToList();
            return Task.FromResult(result);
        }
    }
}
