using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}
