namespace KBS.Web.Model
{
    public class UpdateProfileRequest
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set;}
        public string Address { get; set; }
    }
}