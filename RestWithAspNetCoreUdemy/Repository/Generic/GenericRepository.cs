using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using RestWithAspNetCoreUdemy.Models.Base;
using RestWithAspNetCoreUdemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MySqlContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T model)
        {
            try
            {
                _dataset.Add(model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        public void Delete(long id)
        {
            var result = _dataset.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null) _dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T model)
        {
            if (!Exists(model.Id)) return null;

            var result = _dataset.SingleOrDefault(b => b.Id == model.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(model);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public bool Exists(long? id)
        {
            return _dataset.Any(b => b.Id.Equals(id));
        }
    }
}
