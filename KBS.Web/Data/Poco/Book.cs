using System;

namespace KBS.Web.Data.Poco {
    public class Book {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }
    }
}
