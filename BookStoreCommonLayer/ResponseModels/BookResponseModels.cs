//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Book Response Models
//

namespace BookStoreCommonLayer.ResponseModels
{
    public class BookResponse
    {
        public int BookID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Language { get; set; }

        public string Category { get; set; }

        public string ISBN { get; set; }

        public string Pages { get; set; }
    }
}
