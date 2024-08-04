using UserCRUD.API.DAL.Models;

namespace UserCRUD.API.DAL.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        User GetUserById(int id);
        IEnumerable<User> GetUsers();
        void Update(User user);
        void Delete(int id);
    }
}
