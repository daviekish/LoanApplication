using DomainLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UserService <T>: IUserService<Loans> where T : class
    {
        private readonly IRepository<Loans> iRepository;

        public UserService(IRepository<Loans> iRepository)
        {
            this.iRepository = iRepository;
        }

        public async Task Delete (long id)
        {
            Loans entity = await iRepository.Get(id);
            iRepository.Remove(entity);
            await iRepository.SaveChanges();
        }

        public async Task<Loans> Get(long id)
        {
            return await iRepository.Get(id);
        }

        public IEnumerable<Loans> GetAll()
        {
            return iRepository.GetAll();
        }

        public async Task Insert(Loans entity)
        {
            await iRepository.Insert(entity);
        }

        public async Task Update(Loans entity)
        {
            await iRepository.Update(entity);
        }
    }
}
