using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Data.EF
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext bookStoreContext;
        private readonly ILogger<BookRepository> logger;

        public BookRepository(BookStoreContext bookStoreContext, ILogger<BookRepository> logger)
        {
            this.bookStoreContext = bookStoreContext;
            this.logger = logger;
        }

        public async Task<IList<BookEntity>> GetBooksAsync()
        {
            logger.LogInformation($"********** {nameof(GetBooksAsync)}");
            return await bookStoreContext.Books.ToListAsync();
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            logger.LogInformation($"********** {nameof(GetBookByIdAsync)}");
            return await bookStoreContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IList<BookEntity>> GetBooksByAuthorIdAsync(int authorId)
        {
            logger.LogInformation($"********** {nameof(GetBooksByAuthorIdAsync)}");
            return await bookStoreContext.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        }
    }
}
