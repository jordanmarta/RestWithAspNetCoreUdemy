using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Services.Interfaces
{
    public interface IBookService
    {
        Book Create(Book person);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book person);
        void Delete(long id);
    }
}
