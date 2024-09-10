using EventsApp.Database.Enums;

namespace EventsApp.Database.Dtos.Request
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
    }
}
