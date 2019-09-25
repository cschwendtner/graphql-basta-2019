using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Entities;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace BookStore.Data.InMem
{
    public class AuthorRepository : IAuthorRepository
    {
        private static readonly IList<AuthorEntity> authors = new List<AuthorEntity>
        {
            new AuthorEntity { Id = 21, FirstName = "Firstname 21", LastName = "Lastname 21" },
            new AuthorEntity { Id = 22, FirstName = "Firstname 22", LastName = "Lastname 22" },
            new AuthorEntity { Id = 23, FirstName = "Firstname 23", LastName = "Lastname 23" },
        };

        private readonly ILogger<AuthorRepository> logger;

        public AuthorRepository(ILogger<AuthorRepository> logger)
        {
            this.logger = logger;
        }

        public Task<IList<AuthorEntity>> GetAuthorsAsync()
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetAuthorsAsync)}");
            }
            return Task.FromResult(authors);
        }

        public Task<AuthorEntity> GetAuthorByIdAsync(int authorId)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetAuthorByIdAsync)}");
            }
            return Task.FromResult(authors.FirstOrDefault(a => a.Id == authorId));
        }

        public Task<IDictionary<int, AuthorEntity>> GetAuthorsByIdsAsync(IEnumerable<int> authorIds, CancellationToken cancellationToken)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(GetAuthorsByIdsAsync)}");
            }
            IDictionary<int, AuthorEntity> result = authors.Where(a => authorIds.Contains(a.Id)).ToDictionary(a => a.Id);
            return Task.FromResult(result);
        }

        public Task<AuthorEntity> AddAuthorAsync(AuthorEntity newAuthor)
        {
            if (logger != null)
            {
                logger.LogInformation($"********** {nameof(AddAuthorAsync)}");
            }
            var nextId = authors.Max(a => a.Id) + 1;
            newAuthor.Id = nextId;
            authors.Add(newAuthor);

            return Task.FromResult(newAuthor);
        }
    }
}
