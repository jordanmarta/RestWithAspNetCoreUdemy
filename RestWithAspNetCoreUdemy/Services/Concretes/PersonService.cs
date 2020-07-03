using RestWithAspNetCoreUdemy.Data.Converters;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Models.Context;
using RestWithAspNetCoreUdemy.Repository.Generic;
using RestWithAspNetCoreUdemy.Repository.Interfaces;
using RestWithAspNetCoreUdemy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithAspNetCoreUdemy.Services.Concretes
{
    public class PersonService : IPersonService
    {
        private IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        public PersonService(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}
