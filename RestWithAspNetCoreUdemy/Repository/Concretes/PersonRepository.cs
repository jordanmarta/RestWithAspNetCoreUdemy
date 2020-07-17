using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository.Generic;
using RestWithAspNetCoreUdemy.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetCoreUdemy.Repository.Concretes
{
    public class PersonRepository :GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base (context) { }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}
