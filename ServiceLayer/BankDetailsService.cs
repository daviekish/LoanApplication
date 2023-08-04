using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BankDetailsService : IBankDetailsService
    {
        private readonly IRepository<Banks> iRepository;

        public BankDetailsService(IRepository<Banks> iRepository)
        {
            this.iRepository = iRepository;
        }

        public async Task Delete(long id)
        {
            Banks entity = await iRepository.Get(id);
            iRepository.Remove(entity);
            await iRepository.SaveChanges();
        }

        public async Task<Banks> Get(long id)
        {
            return await iRepository.Get(id);
        }

        public IEnumerable<Banks> GetAll()
        {
            return iRepository.GetAll();
        }

        public async Task Insert(Banks entity)
        {
            await iRepository.Insert(entity);
        }

        public async Task Update(Banks entity)
        {
            await iRepository.Update(entity);
        }
    }
}
