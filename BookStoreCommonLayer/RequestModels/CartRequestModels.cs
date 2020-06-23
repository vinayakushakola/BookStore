//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Cart Request Models
//

using System.ComponentModel.DataAnnotations;

namespace BookStoreCommonLayer.RequestModels
{
    public class CartRequest
    {
        [Required]
        public int BookID { get; set; }
    }
}
