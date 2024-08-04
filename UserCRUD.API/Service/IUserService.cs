using UserCRUD.API.DAL.Models;

namespace UserCRUD.API.Service
{
    public interface IUserService
    {
        int CreateUser(string fullName, List<string> addresses, List<string> phoneNumbers);
        User GetUserById(int id);
        IEnumerable<User> GetUsers();
        void UpdateUser(int id, string fullName, List<string> addresses, List<string> phoneNumbers);
        void DeleteUser(int id);
    }
}
