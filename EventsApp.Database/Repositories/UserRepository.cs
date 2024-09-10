using EventsApp.Database.Context;
using EventsApp.Database.Entities;

namespace EventsApp.Database.Repositories
{
    public class UserRepository
    {
        public AppDbContext context { get; set; }

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<User> GetUsers()
        {
            List<User> users = context.Users
            .Where(u => u.DeletedDate == null)
            .OrderBy(u => u.CreatedDate)
            .ToList();
            return users;
        }

        public User GetUserById(int userId)
        {
            User user = context.Users
            .Where(u => u.DeletedDate == null)
            .Where(u => u.UserId == userId)
            .FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public User SaveUser(User newUser)
        {
            newUser.CreatedDate = DateTime.Now;
            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser;
        }

        public User UpdateUserById(User finalUser, int userId)
        {
            User userToUpdate = GetUserById(userId);
            userToUpdate.Name = finalUser.Name;
            userToUpdate.Email = finalUser.Email;
            userToUpdate.Phone = finalUser.Phone;
            userToUpdate.Role = finalUser.Role;
            userToUpdate.UpdatedDate = DateTime.Now;
            context.SaveChanges();
            return userToUpdate;
        }

        public User DeleteUserById(int userId)
        {
            User userToDelete = GetUserById(userId);
            userToDelete.DeletedDate = DateTime.Now;
            context.SaveChanges();
            return userToDelete;
        }
    }
}
