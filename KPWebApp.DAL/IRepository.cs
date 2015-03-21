using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPWebApp.DAL
{
    public interface IRepository<T> :IDisposable where T : class
    {
        T GetById(object id);
        void Insert(T entity);
        void Delete(object id);
        void Update(T entity);
        void Save();
    }
}
