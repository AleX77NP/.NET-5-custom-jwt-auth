using System.Collections;
using System.Linq;
using Planner.Models;

namespace Planner.Data
{
    public class UserRepositoty : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepositoty(UserContext userContext)
        {
            _userContext = userContext;
        }

        public User Create(User user)
        {
            _userContext.Users.Add(user);
            user.Id = _userContext.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            return _userContext.Users.FirstOrDefault(u => u.Email == email);
        }

        User IUserRepository.GetById(int id)
        {
            return _userContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
