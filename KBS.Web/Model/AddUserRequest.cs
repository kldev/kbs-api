namespace KBS.Web.Model {
    public class AddUserRequest {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
