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
    public class BookService : IBookService
    {
        private IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);    
        }

        public Book Update(Book person)
        {
            return _repository.Update(person);
        }
    }
}
