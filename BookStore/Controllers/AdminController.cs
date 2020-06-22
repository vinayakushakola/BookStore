//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It is Controller of Admin
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        private static bool success;
        private static string message;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        /// <summary>
        /// Admin Registration
        /// </summary>
        /// <param name="adminDetails">Admin Registration Details</param>
        /// <returns>If data found return Ok else null or Bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Registration(AdminRegistrationRequest adminDetails)
        {
            try
            {
                var data = await _adminBusiness.AdminRegistration(adminDetails);
                if (data != null)
                {
                    success = true;
                    message = "Admin Account Created Successfully";
                    return Ok(new { success, message, data });
                }
                else
                {
                    message = "No Data Provided";
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}