using EventsApp.Database.Dtos.Common;
using EventsApp.Database.Dtos.Request;
using EventsApp.Database.Entities;
using EventsApp.Database.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventsApp.Core.Services
{
    public class UserService
    {
        public UserRepository userRepository { get; set; }
        private readonly string securityKey;

        public UserService(UserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            securityKey = config["JWT:SecurityKey"];
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

        public UserDto RegisterUser(RegisterRequest registerRequest)
        {
            byte[] passwordSalt = GenerateSalt();
            User newUser = new User();
            newUser.Name = registerRequest.Name;
            newUser.Email = registerRequest.Email;
            newUser.PasswordSalt = Convert.ToBase64String(passwordSalt);
            newUser.PasswordHash = HashPassword(registerRequest.Password, Convert.FromBase64String(newUser.PasswordSalt));
            newUser.Phone = registerRequest.Phone;
            newUser.Role = registerRequest.Role;
            User savedUser = userRepository.SaveUser(newUser);
            UserDto savedUserDto = ConvertToUserDto(savedUser);
            return savedUserDto;
        }

        public string LoginUser(LoginRequest loginRequest)
        {
            User user = userRepository.GetUserByEmail(loginRequest.Email);
            string hashedPassword = HashPassword(loginRequest.Password, Convert.FromBase64String(user.PasswordSalt));
            if (hashedPassword != user.PasswordHash)
            {
                throw new Exception("Invalid password");   
            }
            string token = GetToken(user, user.Role.ToString());
            return token;
        }

        public UserDto UpdateUserById(UserDto finalUserDto, int userId, int loggedInUserId)
        {
            if (userId != loggedInUserId)
            {
                throw new Exception("You can't update the details of another user");
            }
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

        private UserDto ConvertToUserDto(User user)
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

        private User ConvertToUser(UserDto userDto)
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

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            string hassedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));
            return hassedPassword;
        }

        private string GetToken(User user, string role)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            Claim roleClaim = new Claim("role", role);
            Claim idClaim = new Claim("userId", user.UserId.ToString());
            Claim[] claims = new Claim[] { roleClaim, idClaim };
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Issuer = "Backend";
            tokenDescriptor.Audience = "Frontend";
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.Now.AddHours(1);
            tokenDescriptor.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
