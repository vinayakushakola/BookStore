//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Book Repository
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interfaces
{
    public interface IBookRepository
    {
        Task<BookResponse> AddBook(int adminID, BookRequest bookDetails);
    }
}
