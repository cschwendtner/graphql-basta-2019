using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Data.Entities;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BookStore.Data.EF
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext bookStoreContext;
        private readonly ILogger<AuthorEntity> logger;

        public AuthorRepository(BookStoreContext bookStoreContext, ILogger<AuthorEntity> logger)
        {
            this.bookStoreContext = bookStoreContext;
            this.logger = logger;
        }

        public async Task<IList<AuthorEntity>> GetAuthorsAsync()
        {
            logger.LogInformation($"********** {nameof(GetAuthorsAsync)}");
            return await bookStoreContext.Authors.ToListAsync();
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int authorId)
        {
            logger.LogInformation($"********** {nameof(GetAuthorByIdAsync)}");
            return await bookStoreContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<IDictionary<int, AuthorEntity>> GetAuthorsByIdsAsync(IEnumerable<int> authorIds, CancellationToken cancellationToken)
        {
            logger.LogInformation($"********** {nameof(GetAuthorsByIdsAsync)}");
            IDictionary<int, AuthorEntity> result = await bookStoreContext.Authors.Where(a => authorIds.Contains(a.Id)).ToDictionaryAsync(a => a.Id);
            return result;
        }

        public async Task<AuthorEntity> AddAuthorAsync(AuthorEntity newAuthor)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(AddAuthorAsync)}");
            }
            bookStoreContext.Authors.Add(newAuthor);
            await bookStoreContext.SaveChangesAsync();

            return newAuthor;
        }
    }
}
