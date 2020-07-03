using RestWithAspNetCoreUdemy.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T model);
        T FindById(long id);
        List<T> FindAll();
        T Update(T model);
        void Delete(long id);
    }
}
