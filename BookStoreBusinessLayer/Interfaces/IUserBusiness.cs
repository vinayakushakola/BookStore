//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of User Business
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        Task<RegistrationResponse> UserRegistration(RegistrationRequest userDetails);

        Task<RegistrationResponse> UserLogin(LoginRequest loginDetails);

        Task<RegistrationResponse> ForgotPassword(ForgotPasswordRequest forgotPassword);

        Task<bool> ResetPassword(int userID, ResetPasswordRequest resetPassword);
    }
}
