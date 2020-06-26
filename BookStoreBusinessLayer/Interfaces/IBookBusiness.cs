//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Book Business
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interfaces
{
    public interface IBookBusiness
    {
        Task<List<BookResponse>> GetListOfBooks();

        Task<BookResponse> AddBook(int adminID, BookRequest bookDetails);

        Task<List<BookResponse>> SearchBook(BookSearchRequest bookSearch);

        Task<List<BookResponse>> SortBooks();
    }
}
