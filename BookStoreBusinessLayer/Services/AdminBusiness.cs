//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Interacts between admin controller & admin repository
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using BookStoreRepositoryLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Services
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IAdminRepository _adminRepository;

        public AdminBusiness(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<AdminRegistrationResponse> AdminRegistration(AdminRegistrationRequest adminDetails)
        {
            try
            {
                if (adminDetails == null)
                    return null;
                else
                    return await _adminRepository.AdminRegistration(adminDetails);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AdminRegistrationResponse> AdminLogin(AdminLoginRequest loginDetails)
        {
            try
            {
                if (loginDetails == null)
                    return null;
                else
                    return await _adminRepository.AdminLogin(loginDetails);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
