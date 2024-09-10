using EventsApp.Database.Dtos.Common;
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

        public List<UserDto> GetUsers()
        {
            List<User> users = userRepository.GetUsers();
            List<UserDto> userDtos = new List<UserDto>();
            foreach (User user in users)
            {
                UserDto userDto = ConvertToUserDto(user);
                userDtos.Add(userDto);
            }
            return userDtos;
        }

        public UserDto GetUserById(int userId)
        {
            User user = userRepository.GetUserById(userId);
            UserDto userDto = ConvertToUserDto(user);
            return userDto;
        }

        public UserDto SaveUser(UserDto newUserDto)
        {
            User newUser = ConvertToUser(newUserDto);
            User savedUser = userRepository.SaveUser(newUser);
            UserDto savedUserDto = ConvertToUserDto(savedUser);
            return savedUserDto;
        }

        public UserDto UpdateUserById(UserDto finalUserDto, int userId)
        {
            User finalUser = ConvertToUser(finalUserDto);
            User updatedUser = userRepository.UpdateUserById(finalUser, userId);
            UserDto updatedUserDto = ConvertToUserDto(updatedUser);
            return updatedUserDto;
        }

        public UserDto DeleteUserById(int userId)
        {
            User deletedUser = userRepository.DeleteUserById(userId);
            UserDto deletedUserDto = ConvertToUserDto(deletedUser);
            return deletedUserDto;
        }

        public UserDto ConvertToUserDto(User user)
        {
            UserDto userDto = new UserDto();
            userDto.UserId = user.UserId;
            userDto.Name = user.Name;
            userDto.Email = user.Email;
            userDto.Phone = user.Phone;
            userDto.Role = user.Role;
            userDto.CreatedDate = user.CreatedDate;
            userDto.UpdatedDate = user.UpdatedDate;
            userDto.DeletedDate = user.DeletedDate;
            return userDto;
        }

        public User ConvertToUser(UserDto userDto)
        {
            User user = new User();
            user.UserId = userDto.UserId;
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Role = userDto.Role;
            user.CreatedDate = userDto.CreatedDate;
            user.UpdatedDate = userDto.UpdatedDate;
            user.DeletedDate = userDto.DeletedDate;
            return user;
        }
    }
}
