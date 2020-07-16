using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository.Interfaces;
using System;
using System.Linq;

namespace RestWithAspNetCoreUdemy.Repository.Concretes
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;

        public UserRepository(MySqlContext context)
        {
            _context = context;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}
