//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Wish List Response Models
//

using System.Collections.Generic;

namespace BookStoreCommonLayer.ResponseModels
{
    public class WishListResponse
    {
        public int WishListID { get; set; }

        public string Name { get; set; }
    }

    public class WishListsResponnse
    {
        public List<WishListResponse2> WishLists { get; set; }
    }

    public class WishListResponse2
    {
        public int WishListID { get; set; }

        public string Name { get; set; }

        public List<BookResponse> Books { get; set; }
    }
}
