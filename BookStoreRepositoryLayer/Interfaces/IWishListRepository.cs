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
        Task<WishListsResponnse> GetListOfWishList(int userID);

        Task<List<BookResponse>> GetListOfBooksInWishList(int userID, int wishListID);

        Task<WishListResponse> CreateNewWishList(int userID, WishListRequest wishList);

        Task<BookResponse> AddBookIntoWishList(int userID, int wishListID, WishListBookRequest wishListBook);

        Task<bool> DeleteBookFromWishList(int userID, int wishListID, WishListBookRequest wishListBook);
    }
}
