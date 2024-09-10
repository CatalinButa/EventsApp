using EventsApp.Database.Entities;
using EventsApp.Database.Enums;

namespace EventsApp.Database.Dtos.Common
{
    public class UserDto : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
    }
}
