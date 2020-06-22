//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : Interface of Admin Business
//

using BookStoreCommonLayer.RequestModels;
using BookStoreCommonLayer.ResponseModels;
using System.Threading.Tasks;

namespace BookStoreBusinessLayer.Interfaces
{
    public interface IAdminBusiness
    {
        Task<AdminRegistrationResponse> AdminRegistration(AdminRegistrationRequest adminDetails);
    }
}
