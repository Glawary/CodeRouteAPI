using CodeRoute.Models;

namespace CodeRoute.DAL.Repositories
{
    public class UserRepository
    {
        private readonly IContext _context;
        private Logger<UserRepository> _logger;
        public UserRepository(IContext context, Logger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserByUserName(User user)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName);
        }

        public bool AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "Error with add new user in db", null);
                return false;
            }

            return true;
        }

        public User FindUser(string username, string usermail)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username || u.Email == usermail);
        }
    }
}
