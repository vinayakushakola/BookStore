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

        public async Task<WishListsResponnse> GetListOfWishList(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.GetListOfWishList(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookResponse>> GetListOfBooksInWishList(int userID, int wishListID)
        {
            try
            {
                if (wishListID <= 0 || userID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.GetListOfBooksInWishList(userID, wishListID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WishListResponse> CreateNewWishList(int userID, WishListRequest wishList)
        {
            try
            {
                if (userID <= 0 || wishList == null)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.CreateNewWishList(userID, wishList);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookResponse> AddBookIntoWishList(int userID, int wishListID, WishListBookRequest wishListBook)
        {
            try
            {
                if (userID <= 0 || wishListBook == null)
                {
                    return null;
                }
                else
                {
                    return await _wishListRepository.AddBookIntoWishList(userID, wishListID, wishListBook);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBookFromWishList(int userID, int wishListID, WishListBookRequest wishListBook)
        {
            try
            {
                if (userID <= 0 || wishListBook == null)
                {
                    return false;
                }
                else
                {
                    return await _wishListRepository.DeleteBookFromWishList(userID, wishListID, wishListBook);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
