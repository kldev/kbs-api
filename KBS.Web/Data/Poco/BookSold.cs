using System;

namespace KBS.Web.Data.Poco {
    public class BookSold {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime SoldDate { get; set; }
    }
}
