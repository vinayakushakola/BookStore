//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It is Controller of Cart
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness _cartBusiness;

        private static bool success;
        private static string message;

        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        /// <summary>
        /// Add Book into Cart
        /// </summary>
        /// <param name="cart">Cart Data</param>
        /// <returns>If Data Found return Ok else Not Found or Bad Request</returns>
        [HttpPost]
        public async Task<IActionResult> AddBookIntoCart(CartRequest cart)
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
                        var data = await _cartBusiness.AddBookIntoCart(userID, cart);
                        if (data != null)
                        {
                            success = true;
                            message = "Book Added into Cart Successfully";
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
    }
}