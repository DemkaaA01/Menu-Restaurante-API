﻿using Menu_Restaurante_API.Data;
using Menu_Restaurante_API.Entities;
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

        public User GetByID(int userId)
        {
            return _context.Users.SingleOrDefault(x => x.Id == userId);
        }
        public List<User> GetAll() 
        { 
            return _context.Users.ToList();
        }
        public User ValidateUser(string email, string password)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);              
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

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.SaveChanges();
        }
        public void Delete(int userId) 
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    }
}
