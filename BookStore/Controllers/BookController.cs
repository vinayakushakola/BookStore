//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It is Controller of Book
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        private static bool success;
        private static string message;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        /// <summary>
        /// Shows All the Books
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        public async Task<IActionResult> GetListOfBooks()
        {
            try
            {
                var data = await _bookBusiness.GetListOfBooks();
                if (data != null)
                {
                    success = true;
                    message = "List of Books Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Found";
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Add Book Details
        /// </summary>
        /// <param name="book">Book Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook(BookRequest book)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "Admin"))
                    {
                        int adminID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "AdminID").Value);
                        var data = await _bookBusiness.AddBook(adminID, book);
                        if (data != null)
                        {
                            success = true;
                            message = "Book Details Added Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Provided";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "You are not allowed to add Book, Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Search Book by Name
        /// </summary>
        /// <param name="bookSearch">Book Search Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> BookSearch(BookSearchRequest bookSearch)
        {
            try
            {
                var data = await _bookBusiness.SearchBook(bookSearch);
                if (data != null)
                {
                    success = true;
                    message = "Book Fetched Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "Book Not Found";
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}