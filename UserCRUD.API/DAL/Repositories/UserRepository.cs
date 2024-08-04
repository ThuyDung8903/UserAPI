using Microsoft.EntityFrameworkCore;
using UserCRUD.API.DAL.Models;

namespace UserCRUD.API.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var user = _context.Users.Include(u => u.Addresses)
                    .Include(u => u.PhoneNumbers)
                    .FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Include(u => u.Addresses)
                    .Include(u => u.PhoneNumbers)
                    .FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _context.Users.Include(u => u.Addresses)
                    .Include(u => u.PhoneNumbers)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
