using System;

namespace KBS.Web.Data.Entity {
    public class BookSoldItem {
        public String Id { get; set; }

        public String UserId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public DateTime SoldDate { get; set; }
    }
}
