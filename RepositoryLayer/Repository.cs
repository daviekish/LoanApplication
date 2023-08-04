using DomainLayer;
using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LoanApplicationDBContext context;
        private DbSet<T> entities;

        public Repository(LoanApplicationDBContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> Get(long id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Remove(T entity)
        {

            if (entity == null)
            {

                throw new ArgumentNullException("entity");

            }
            entities.Remove(entity);
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            await context.SaveChangesAsync();
        }
    }
}
