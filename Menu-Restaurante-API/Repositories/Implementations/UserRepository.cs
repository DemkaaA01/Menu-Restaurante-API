using Menu_Restaurante_API.Data;
using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Helpers;
using Menu_Restaurante_API.Repositories.Interfaces;

namespace Menu_Restaurante_API.Repositories.Implementations
{
    public class UserRepository:IUserRepository
    {
        private readonly MenuRestauranteContext _context;

        public UserRepository(MenuRestauranteContext context) 
        {
            _context = context;
        }

        public User GetById(int userId)
        {
            return _context.Users.SingleOrDefault(x => x.Id == userId);
        }
        public List<User> GetAll() 
        { 
            return _context.Users.ToList();
        }
        public User ValidateUser(string email, string password)
        {
            var hashed = PasswordHelper.Hash(password);
            return _context.Users.SingleOrDefault(x => x.Email == email && x.Password == hashed);              
        }
        public int Create(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }

        public void Update(User updatedUser, int userId)
        {
            var user = _context.Users.SingleOrDefault( x => x.Id == userId); 
            if (user == null) return;

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.SaveChanges();
        }
        public void Remove(int userId) 
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    }
}
