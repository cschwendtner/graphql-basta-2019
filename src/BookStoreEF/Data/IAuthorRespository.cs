using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public interface IAuthorRepository
    {
        Task<IList<AuthorEntity>> GetAuthorsAsync();

        Task<AuthorEntity> GetAuthorByIdAsync(int authorId);

        Task<IDictionary<int, AuthorEntity>> GetAuthorsByIdsAsync(IEnumerable<int> authorIds, CancellationToken cancellationToken);

        Task<AuthorEntity> AddAuthorAsync(AuthorEntity newAuthor);
    }
}
