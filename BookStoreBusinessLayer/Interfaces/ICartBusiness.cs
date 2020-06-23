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
        Task<List<BookResponse>> GetListOfBooksInCart(int userID);
        
        Task<BookResponse> AddBookIntoCart(int userID, CartRequest cart);

        Task<bool> DeleteBookFromCart(int userID, CartRequest cart);
    }
}
