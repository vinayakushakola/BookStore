//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It is Controller of Wish List
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
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBusiness _wishListBusiness;

        private static bool success;
        private static string message;

        public WishListController(IWishListBusiness wishListBusiness)
        {
            _wishListBusiness = wishListBusiness;
        }

        /// <summary>
        /// Shows All Books in Wish List
        /// </summary>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet]
        public async Task<IActionResult> GetListOfWishList()
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.GetListOfWishList(userID);
                        if (data != null)
                        {
                            success = true;
                            message = "List of Books in Wish List Fetched Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Found";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Shows Books by WishListID
        /// </summary>
        /// <param name="wishListID">wishListID</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpGet("{wishListID}")]
        public async Task<IActionResult> GetListOfBooksInWishList(int wishListID)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.GetListOfBooksInWishList(userID, wishListID);
                        if (data != null)
                        {
                            success = true;
                            message = "List of Books in Wish List Fetched Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Found";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Creates New Wish List
        /// </summary>
        /// <param name="wishList">Wish List Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewWishList(WishListRequest wishList)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.CreateNewWishList(userID, wishList);
                        if (data != null)
                        {
                            success = true;
                            message = "New Wish List Created Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Provided";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "You should login first before creating new wish list, Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Add Book into Wish List
        /// </summary>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost("{wishListID}")]
        public async Task<IActionResult> AddBookIntoWishList(int wishListID, WishListBookRequest wishListBook)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.AddBookIntoWishList(userID, wishListID, wishListBook);
                        if (data != null)
                        {
                            success = true;
                            message = "Book Added into Wish List Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Data Provided";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "You should login first before adding into cart, Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Delete Book From Wish List
        /// </summary>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Deleted return Ok else Not Found or Bad Request</returns>
        [HttpDelete("{wishListID}")]
        public async Task<IActionResult> DeleteBookFromCart(int wishListID, WishListBookRequest wishListBook)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.DeleteBookFromWishList(userID, wishListID, wishListBook);
                        if (data)
                        {
                            success = true;
                            message = "Book Removed from Wish List Successfully";
                            return Ok(new { success, message });
                        }
                        else
                        {
                            message = "No Book is present with this ID: ";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Move Book to Cart
        /// </summary>
        /// <param name="wishListID">WishListID</param>
        /// <param name="wishListBook">Wish List Book Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost("{wishListID}/Move")]
        public async Task<IActionResult> MoveToCart(int wishListID, WishListBookRequest wishListBook)
        {
            try
            {
                var user = HttpContext.User;
                if ((user.HasClaim(u => u.Type == "TokenType")) && (user.HasClaim(u => u.Type == "UserRole")))
                {
                    if ((user.Claims.FirstOrDefault(u => u.Type == "TokenType").Value == "login") &&
                            (user.Claims.FirstOrDefault(u => u.Type == "UserRole").Value == "User"))
                    {
                        int userID = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserID").Value);
                        var data = await _wishListBusiness.MoveToCart(userID, wishListID, wishListBook);
                        if (data != null)
                        {
                            success = true;
                            message = "Book Moved to Cart Successfully";
                            return Ok(new { success, message, data });
                        }
                        else
                        {
                            message = "No Book is Present";
                            return NotFound(new { success, message });
                        }
                    }
                }
                message = "Token Invalid!";
                return BadRequest(new { success, message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}