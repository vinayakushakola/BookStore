//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Wish List Request Models
//

using System.ComponentModel.DataAnnotations;

namespace BookStoreCommonLayer.RequestModels
{
    public class WishListRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class WishListBookRequest
    {
        [Required]
        public int WishListID { get; set; }

        [Required]
        public int BookID { get; set; }
    }
}
