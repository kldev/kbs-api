using System;

namespace KBS.Web.Data.Poco {
    public class User {

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }

        public int IsDeleted { get; set; } = 0;
    }
}
