//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of User Repository
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<RegistrationResponse> UserRegistration(RegistrationRequest userDetails);

        Task<RegistrationResponse> UserLogin(LoginRequest loginDetails);

        Task<RegistrationResponse> ForgotPassword(ForgotPasswordRequest forgotPassword);

        Task<bool> ResetPassword(int userID, ResetPasswordRequest resetPassword);
    }
}
