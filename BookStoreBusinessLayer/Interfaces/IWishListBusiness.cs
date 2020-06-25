//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Wish List Business
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interfaces
{
    public interface IWishListBusiness
    {
        Task<WishListsResponnse> GetListOfWishList(int userID);

        Task<List<BookResponse>> GetListOfBooksInWishList(int userID, int wishListID);

        Task<WishListResponse> CreateNewWishList(int userID, WishListRequest wishList);

        Task<BookResponse> AddBookIntoWishList(int userID, int wishListID, WishListBookRequest wishListBook);

        Task<bool> DeleteBookFromWishList(int userID, int wishListID, WishListBookRequest wishListBook);

        Task<CartBookResponse> MoveToCart(int userID, int wishListID, WishListBookRequest wishListBook);
    }
}
