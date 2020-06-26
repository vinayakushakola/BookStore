//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : It Contain Book Request Models
//

using System.ComponentModel.DataAnnotations;

namespace BookStoreCommonLayer.RequestModels
{
    public class BookRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Pages { get; set; }
    }

    public class BookSearchRequest
    {
        [Required]
        public string Search { get; set; }
    }
}
