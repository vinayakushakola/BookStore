//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It Interacts between user controller & user repository
//

using BookStoreBusinessLayer.Interfaces;
using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using BookStoreRepositoryLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegistrationResponse> UserRegistration(RegistrationRequest userDetails)
        {
            if (userDetails == null)
                return null;
            else
                return await _userRepository.UserRegistration(userDetails);
        }

        public Task<RegistrationResponse> UserLogin(LoginRequest loginDetails)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponse> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(ResetPasswordRequest resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
