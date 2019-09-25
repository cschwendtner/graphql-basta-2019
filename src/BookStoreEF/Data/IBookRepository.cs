using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public interface IBookRepository
    {
        Task<IList<BookEntity>> GetBooksAsync();

        Task<BookEntity> GetBookByIdAsync(int bookId);

        Task<IList<BookEntity>> GetBooksByAuthorIdAsync(int authorId);
    }
}
