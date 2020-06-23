//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Wish List Repository
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interfaces
{
    public interface IWishListRepository
    {
        Task<List<BookResponse>> GetListOfBooksInWishList(int userID);

        Task<BookResponse> AddBookIntoWishList(int userID, WishListRequest wishList);

        Task<bool> DeleteBookFromWishList(int userID, WishListRequest wishList);
    }
}
