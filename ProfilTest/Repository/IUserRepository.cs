using ProfilTest.Data;
using ProfilTest.Models;

namespace ProfilTest.Repository
{
    public interface IUserRepository
    {
        Users GetUserByUsername(string username);
        List<Users> GetUsers();
        void AddUser(Users user);
    }



    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Users GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(u => u.UserName == username);
        }
        public List<Users> GetUsers() 
        {
            return _context.User.ToList();
        }
        public void AddUser(Users user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }



}
