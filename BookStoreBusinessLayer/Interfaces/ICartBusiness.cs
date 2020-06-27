//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Cart Business
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interfaces
{
    public interface ICartBusiness
    {
        Task<List<CartBookResponse>> GetListOfBooksInCart(int userID);
        
        Task<CartBookResponse> AddBookIntoCart(int userID, CartRequest cart);

        Task<bool> DeleteBookFromCart(int userID, int cartID);

        Task<PurchaseResponse> Purchase(int userID, int cartID, PurchaseRequest purchase);
    }
}
