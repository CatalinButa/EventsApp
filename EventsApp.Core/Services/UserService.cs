using EventsApp.Database.Entities;
using EventsApp.Database.Repositories;

namespace EventsApp.Core.Services
{
    public class UserService
    {
        public UserRepository userRepository { get; set; }

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            List<User> users = userRepository.GetUsers();
            return users;
        }

        public User GetUserById(int userId)
        {
            User user = userRepository.GetUserById(userId);
            return user;
        }

        public User SaveUser(User newUser)
        {
            User savedUser = userRepository.SaveUser(newUser);
            return savedUser;
        }

        public User UpdateUserById(User finalUser, int userId)
        {
            User updatedUser = userRepository.UpdateUserById(finalUser, userId);
            return updatedUser;
        }

        public User DeleteUserById(int userId)
        {
            User deletedUser = userRepository.DeleteUserById(userId);
            return deletedUser;
        }
    }
}
