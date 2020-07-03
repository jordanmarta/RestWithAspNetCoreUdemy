using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Models;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Services.Interfaces
{
    public interface IPersonService
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
