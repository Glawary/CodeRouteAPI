using CodeRoute.DTO;
using CodeRoute.Models;
using CodeRoute.Repositories;

namespace CodeRoute.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(UserLogInfo user)
        {
            User newUser = new User()
            {
                Email = user.EMail,
                Password = user.Password,
                UserName = user.UserName,
                IsAdmin = false
            };

            return _userRepository.AddUser(newUser);
        }
    }
}
