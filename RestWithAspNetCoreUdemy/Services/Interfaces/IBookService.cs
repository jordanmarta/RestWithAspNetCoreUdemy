using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Services.Interfaces
{
    public interface IBookService
    {
        BookVO Create(BookVO person);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO person);
        void Delete(long id);
    }
}
