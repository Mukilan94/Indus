using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Find(object id);
        void Delete(T te);
        void Add(T te);
        void Edit(T te);

    }
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Db;
        public void Add(T te)
        {
            Db.Set<T>().Add(te);
        }

        public void Delete(T te)
        {
            Db.Entry<T>(te).State = EntityState.Modified;
        }

        public void Edit(T te)
        {
            Db.Entry<T>(te).State = EntityState.Modified;
        }

        public T Find(object id)
        {
            return Db.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {

            return Db.Set<T>();
        }
    }
}
