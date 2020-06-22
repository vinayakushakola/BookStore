//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain User Response Models
//

using System;

namespace BookStoreCommonLayer.ResponseModels
{
    public class RegistrationResponse
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string UserRole { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
