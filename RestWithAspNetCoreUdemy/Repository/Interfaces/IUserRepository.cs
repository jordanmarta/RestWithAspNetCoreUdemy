using RestWithAspNetCoreUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNetCoreUdemy.Repository.Interfaces
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}
