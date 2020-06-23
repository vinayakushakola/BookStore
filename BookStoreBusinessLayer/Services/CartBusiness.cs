﻿//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Interacts between cart controller & cart repository
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
    public class CartBusiness : ICartBusiness
    {
        private readonly ICartRepository _cartRepository;

        public CartBusiness(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<BookResponse>> GetListOfBooksInCart(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    return null;
                }
                else
                {
                    return await _cartRepository.GetListOfBooksInCart(userID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookResponse> AddBookIntoCart(int userID, CartRequest cart)
        {
            try
            {
                if (userID <= 0 || cart == null)
                {
                    return null;
                }
                else
                {
                    return await _cartRepository.AddBookIntoCart(userID, cart);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
