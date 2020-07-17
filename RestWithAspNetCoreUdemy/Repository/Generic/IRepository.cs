using RestWithAspNetCoreUdemy.Models.Base;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T model);
        T FindById(long id);
        List<T> FindAll();
        T Update(T model);
        void Delete(long id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
