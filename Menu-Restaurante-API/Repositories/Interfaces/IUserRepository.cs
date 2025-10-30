using Menu_Restaurante_API.Entities;

namespace Menu_Restaurante_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int userId);
        List<User> GetAll();
        User ValidateUser(string email, string password); // login
        int Create(User newUser);
        void Update(User updatedUser, int userId);
        void Remove(int userId);
    }
}
