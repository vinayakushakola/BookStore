//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It is Controller of User
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;

        private static bool success;
        private static string message;
        private static string token;

        public UserController(IUserBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationRequest userDetails)
        {
            try
            {
                if (!ValidateRegisterRequest(userDetails))
                    return BadRequest(new { Message = "Enter Proper Data" });

                var data = await _userBusiness.UserRegistration(userDetails);
                if (data != null)
                {
                    success = true;
                    message = "User Account Created Successfully";
                    token = GenerateToken(data, "Registration");
                    return Ok(new { success, message, data, token });
                }
                else
                {
                    message = "No Data Provided";
                    return NotFound(new { success, message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="userDetails">User Details</param>
        /// <param name="tokenType">Token Type</param>
        /// <returns>It returns token</returns>
        private string GenerateToken(RegistrationResponse userDetails, string tokenType)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("UserID", userDetails.UserID.ToString()),
                    new Claim("Email", userDetails.Email.ToString()),
                    new Claim("TokenType", tokenType),
                    new Claim("UserRole", userDetails.UserRole.ToString())
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"],
                    claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// It Validate Registration Request
        /// </summary>
        /// <param name="userDetails">User Details</param>
        /// <returns>If validation successfull return true else false</returns>
        private bool ValidateRegisterRequest(RegistrationRequest userDetails)
        {
            if (userDetails == null || string.IsNullOrWhiteSpace(userDetails.FirstName) ||
                    string.IsNullOrWhiteSpace(userDetails.LastName) || string.IsNullOrWhiteSpace(userDetails.Email) ||
                    string.IsNullOrWhiteSpace(userDetails.Password) ||
                    userDetails.FirstName.Length < 3 || userDetails.LastName.Length < 3 || !userDetails.Email.Contains('@') ||
                    !userDetails.Email.Contains('.') || userDetails.Password.Length < 8 )
                return false;
            else
                return true;
        }
    }
}