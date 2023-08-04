using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        Task<T> Get(long id);

        Task Insert(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        void Remove(T entity);

        Task SaveChanges();
    }
}
