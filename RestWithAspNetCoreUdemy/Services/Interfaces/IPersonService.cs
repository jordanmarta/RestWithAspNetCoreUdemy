﻿using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Services.Interfaces
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
