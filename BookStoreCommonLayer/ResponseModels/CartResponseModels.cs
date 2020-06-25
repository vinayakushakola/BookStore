//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Cart Response Models
//

namespace BookStoreCommonLayer.ResponseModels
{
    public class CartBookResponse
    {
        public int CartID { get; set; }

        public int BookID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Language { get; set; }

        public string Category { get; set; }

        public string ISBN { get; set; }

        public string Pages { get; set; }
    }

    public class PurchaseResponse
    {
        public int PurchaseID { get; set; }

        public int BookID { get; set; }

        public string Address { get; set; }
    }
}
