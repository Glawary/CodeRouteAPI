using CodeRoute.Models;

namespace CodeRoute.Repositories
{
    public class UserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUserName(User user)
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
                return false;
            }

            return true;
        }
    }
}
