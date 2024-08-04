using UserCRUD.API.DAL.Models;
using UserCRUD.API.DAL.Repositories;

namespace UserCRUD.API.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public int CreateUser(string fullName, List<string> addresses, List<string> phoneNumbers)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = new User
                    {
                        FullName = fullName
                    };
                    foreach (var address in addresses)
                    {
                        user.Addresses.Add(new Address { Location = address });
                    }
                    foreach (var phoneNumber in phoneNumbers)
                    {
                        user.PhoneNumbers.Add(new PhoneNumber { Number = phoneNumber });
                    }
                    _userRepository.Create(user);
                    transaction.Commit();
                    return user.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _userRepository.Delete(id);
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
                return _userRepository.GetUserById(id);
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
                return _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(int id, string fullName, List<string> addresses, List<string> phoneNumbers)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _userRepository.GetUserById(id);
                    if (user == null)
                    {
                        throw new Exception("User not found");
                    }
                    user.FullName = fullName;
                    user.UpdatedAt = DateTime.UtcNow;
                    user.Addresses.Clear();
                    user.PhoneNumbers.Clear();
                    foreach (var address in addresses)
                    {
                        user.Addresses.Add(new Address { Location = address });
                    }
                    foreach (var phoneNumber in phoneNumbers)
                    {
                        user.PhoneNumbers.Add(new PhoneNumber { Number = phoneNumber });
                    }
                    _userRepository.Update(user);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
