using CodeRoute.DAL.Repositories;
using CodeRoute.DTO;
using CodeRoute.Models;

namespace CodeRoute.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public bool RegUser(UserLogInfo user)
        {
            User newUser = new User()
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                IsAdmin = false
            };

            return _userRepository.AddUser(newUser);
        }

        public User AuthUser(UserLogInfo user)
        {
            User newUser = _userRepository.FindUser(user.UserName, user.Email);

            return newUser;
        }
    }
}
