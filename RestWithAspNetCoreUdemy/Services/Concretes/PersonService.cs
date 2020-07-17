using RestWithAspNetCoreUdemy.Data.Converters;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Repository.Generic;
using RestWithAspNetCoreUdemy.Services.Interfaces;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithAspNetCoreUdemy.Services.Concretes
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonService(IPersonRepository repository)
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
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

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;

            var query = $@"SELECT
                            * 
                          FROM 
                             persons p
                             {(!string.IsNullOrEmpty(name) ? 
                                    $"WHERE p.FirstName like '%{name}%'" :  "")}
                          ORDER BY
                              p.FirstName {sortDirection} limit {pageSize} offset {page}";

            var countQuery = $@"SELECT
                                  COUNT(Id) 
                                FROM 
                                   persons p
                                   {(!string.IsNullOrEmpty(name) ?
                              $"WHERE p.FirstName like '%{name}%'" : "")}";

            var persons = _converter.ParseList(_repository.FindWithPagedSearch(query));

            var totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }

}
