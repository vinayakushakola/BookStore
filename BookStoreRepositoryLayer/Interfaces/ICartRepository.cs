//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Cart Repository
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interfaces
{
    public interface ICartRepository
    {
        Task<List<BookResponse>> GetListOfBooksInCart(int userID);

        Task<BookResponse> AddBookIntoCart(int userID, CartRequest cart);
    }
}
