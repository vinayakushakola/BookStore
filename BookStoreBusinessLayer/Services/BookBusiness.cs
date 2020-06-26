//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Interacts between book controller & book repository
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
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepository _bookRepository;

        public BookBusiness(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookResponse>> GetListOfBooks()
        {
            try
            {
                return await _bookRepository.GetListOfBooks();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<BookResponse> AddBook(int adminID, BookRequest bookDetails)
        {
            try
            {
                if (bookDetails == null)
                    return null;
                else
                    return await _bookRepository.AddBook(adminID, bookDetails);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookResponse>> SearchBook(BookSearchRequest bookSearch)
        {
            try
            {
                if (bookSearch == null)
                    return null;
                else
                    return await _bookRepository.BookSearch(bookSearch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
