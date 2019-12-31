using System;

namespace KBS.Web.Data.Poco {
    public class UserProfile {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
