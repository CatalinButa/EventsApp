﻿using EventsApp.Database.Enums;

namespace EventsApp.Database.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
    }
}
