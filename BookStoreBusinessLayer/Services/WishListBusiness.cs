//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Interacts between wish list controller & wish list repository
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using BookStoreRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Services
{
    public class WishListBusiness : IWishListBusiness
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListBusiness(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        public async Task<List<BookResponse>> GetListOfBooksInWishList(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.GetListOfBooksInWishList(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookResponse> AddBookIntoWishList(int userID, WishListRequest wishList)
        {
            try
            {
                if (userID <= 0 || wishList.BookID == 0)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.AddBookIntoWishList(userID, wishList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBookFromWishList(int userID, WishListRequest wishList)
        {
            try
            {
                if (userID <= 0 || wishList.BookID <= 0)
                {
                    return false;
                }
                else
                {
                    return await _wishListRepository.DeleteBookFromWishList(userID, wishList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
