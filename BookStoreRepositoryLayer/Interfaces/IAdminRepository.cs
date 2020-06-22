//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Admin Repository
//


using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.Interfaces
{
    public interface IAdminRepository
    {
        Task<AdminRegistrationResponse> AdminRegistration(AdminRegistrationRequest adminDetails);
    }
}
