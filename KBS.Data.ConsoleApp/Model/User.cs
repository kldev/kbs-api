using System;

namespace KBS.Data.ConsoleApp.Model {
    public class User {

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }

        public int IsDeleted { get; set; } = 0;
    }
}
