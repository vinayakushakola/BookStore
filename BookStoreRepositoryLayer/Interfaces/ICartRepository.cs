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
        Task<List<CartBookResponse>> GetListOfBooksInCart(int userID);

        Task<CartBookResponse> AddBookIntoCart(int userID, CartRequest cart);

        Task<bool> DeleteBookFromCart(int userID, int cartID);

        Task<PurchaseResponse> Purchase(int userID, int cartID, PurchaseRequest purchase);
    }
}
